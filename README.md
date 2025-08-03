# Minesweeper_Project
Привіт!
Це класичний **Minesweeper** (Сапер), реалізований як веб-застосунок на ASP.NET Core MVC з поділом на UI, API, BLL, DAL, Domain та збереженням результатів у базі даних.

## Функціонал
- Введення імені гравця, розміру поля та кількості мін
- Відкриття клітинок, прапорці, виграш/програш
- Класичний інтерфейс у стилі Windows 98
- Смайлик, що змінюється залежно від результату
- Кнопка `Help` з правилами гри
- Збереження та відображення рейтингу
- Алгоритм-розв’язувач (Solver)

## 1. Як запустити
Скачати або клонувати репозиторій:
   git clone https://github.com/Yurassic10/Minesweeper_Project.git

## 2. Створи базу даних
1. Відкрий Microsoft SQL Server Management Studio або Azure Data Studio
2. Відкрий файл `CreateDatabase.sql`
3. Виконай скрипт — він створить усі таблиці та базу `MinesweeperDB`

### 3. Змiни рядок підключення до бази даних
У файлі `appsettings.json` заміни цей блок на свій власний:
"ConnectionStrings": {
  "DefaultConnection": "..."
}
