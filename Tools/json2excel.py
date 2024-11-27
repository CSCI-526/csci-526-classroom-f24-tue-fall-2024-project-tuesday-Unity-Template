import json
import pandas as pd
import numpy as np
from openpyxl.utils import get_column_letter

# Constants
TOTAL_WAVES = 5


def process_game_data(json_path):
    # Read JSON
    with open(json_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    # Store List
    records = []

    for record_id, record_data in data["gameResults"].items():
        main_record = {
            "id": record_id,
            "level": record_data.get("level", "unknown"),  # Add level field
            "finalWave": record_data.get("finalWave"),
            "flashlightUsageCount": record_data.get("flashlightUsageCount"),
            "result": record_data.get("result"),
            "timestamp": record_data.get("timestamp"),
            "totalGameTime": record_data.get("totalGameTime"),
        }

        # Read Tower Data
        if "towerData" in record_data:
            for i, tower in enumerate(record_data["towerData"], 1):
                main_record[f"tower_{i}_totalKillCount"] = tower.get("totalKillCount")
                main_record[f"tower_{i}_totalChargeTime"] = tower.get("totalChargeTime")
        else:
            main_record["tower_1_totalKillCount"] = None
            main_record["tower_1_totalChargeTime"] = None

        # Read flashlightDurations
        if "flashlightDurations" in record_data:
            for idx, duration in enumerate(record_data["flashlightDurations"], 1):
                main_record[f"duration_{idx}"] = duration
        else:
            main_record["duration_1"] = None

        # Process chargeTimesPerWave
        if "chargeTimesPerWave" in record_data:
            for idx, charge_time in enumerate(record_data["chargeTimesPerWave"], 1):
                main_record[f"chargeTimePerWave_{idx}"] = charge_time
            for idx in range(len(record_data["chargeTimesPerWave"]) + 1, TOTAL_WAVES + 1):
                main_record[f"chargeTimePerWave_{idx}"] = None
        else:
            for idx in range(1, TOTAL_WAVES + 1):
                main_record[f"chargeTimePerWave_{idx}"] = None

        records.append(main_record)

    # Convert to DataFrame
    df = pd.DataFrame(records)

    # Calculate wave statistics per level
    wave_stats = calculate_wave_statistics(df)

    # Reorder columns
    all_columns = list(df.columns)
    total_game_time_index = all_columns.index('totalGameTime')
    duration_columns = sorted(
        [col for col in df.columns if col.startswith('duration_')],
        key=lambda x: int(x.split('_')[1])
    )

    new_columns_order = (
            all_columns[:total_game_time_index + 1] +
            duration_columns +
            [col for col in all_columns[total_game_time_index + 1:] if col not in duration_columns]
    )

    df = df[new_columns_order]

    # Split data by level and save to separate sheets
    with pd.ExcelWriter('output_by_level.xlsx', engine='openpyxl') as writer:
        # Save all data to the first sheet
        df.to_excel(writer, sheet_name='All_Data', index=False)

        # Split and save data by level
        for level in df['level'].unique():
            level_df = df[df['level'] == level]
            sheet_name = f'Level_{level}'
            level_df.to_excel(writer, sheet_name=sheet_name, index=False)

        # Add summary sheet
        summary_df = pd.DataFrame({
            'Level': df['level'].unique(),
            'Total_Games': df.groupby('level').size(),
            'Avg_Game_Time': df.groupby('level')['totalGameTime'].mean(),
            'Avg_Final_Wave': df.groupby('level')['finalWave'].mean()
        }).reset_index(drop=True)

        summary_df.to_excel(writer, sheet_name='Summary', index=False)

        # Save wave statistics to a new sheet
        wave_stats.to_excel(writer, sheet_name='Wave_Statistics', index=False)

        # Adjust column widths for wave statistics
        worksheet = writer.sheets['Wave_Statistics']
        for idx, col in enumerate(wave_stats.columns, 1):
            max_length = max(
                wave_stats[col].astype(str).map(len).max(),
                len(col)
            ) + 2
            column_letter = get_column_letter(idx)
            worksheet.column_dimensions[column_letter].width = max_length

    return df


def calculate_wave_statistics(df):
    stats_records = []

    for level in df['level'].unique():
        level_data = df[df['level'] == level]
        total_games = len(level_data)
        wins = len(level_data[level_data['result'] == 'Win'])
        success_rate = (wins / total_games * 100) if total_games > 0 else 0

        for wave in range(1, TOTAL_WAVES + 1):
            wave_times = level_data[f'chargeTimePerWave_{wave}'].dropna()
            wave_times = wave_times[wave_times > 0]  # 只考虑大于0的充电时间

            if len(wave_times) > 0:
                stats_records.append({
                    'Level': level,
                    'Wave': wave,
                    'Games_Count': total_games,  # 使用总游戏次数
                    'Avg_Charge_Time': wave_times.mean() if len(wave_times) > 0 else 0,
                    'Min_Charge_Time': wave_times.min() if len(wave_times) > 0 else 0,
                    'Max_Charge_Time': wave_times.max() if len(wave_times) > 0 else 0,
                    'Median_Charge_Time': wave_times.median() if len(wave_times) > 0 else 0,
                    'Success_Rate': success_rate  # 使用计算好的成功率
                })
            else:
                # 如果没有有效的充电时间数据，仍然记录基本信息
                stats_records.append({
                    'Level': level,
                    'Wave': wave,
                    'Games_Count': total_games,
                    'Avg_Charge_Time': 0,
                    'Min_Charge_Time': 0,
                    'Max_Charge_Time': 0,
                    'Median_Charge_Time': 0,
                    'Success_Rate': success_rate
                })

    # Convert to DataFrame and round numeric columns
    stats_df = pd.DataFrame(stats_records)
    numeric_columns = ['Avg_Charge_Time', 'Min_Charge_Time', 'Max_Charge_Time',
                       'Median_Charge_Time', 'Success_Rate']
    stats_df[numeric_columns] = stats_df[numeric_columns].round(2)

    # Sort by Level and Wave
    stats_df = stats_df.sort_values(['Level', 'Wave'])

    return stats_df


if __name__ == "__main__":
    df = process_game_data('data.json')
    print("Data processing completed. Check 'output_by_level.xlsx' for results.")