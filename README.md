<h1 align="center">Приложение «MyDelivery» для службы доставки на платформе WinForms</h1>
Приложение разработана для службы доставки и включает в себя несколько ключевых функций для фиксации информации по заказам и выполнение различных операций над ними.


### Основные функции приложения "MyDelivery":
- **Создание и управление заказами:**
  - Возможность добавления новых заказов с уникальным номеров, весом, районом, датой и временем доставки;
  - Проверка корректность введенных данных перед созданием заказа(например, номер заказа уникален, район выбран, вес является положительным числом, дата и время доставки указаны).
- **Интерфейс пользователя:**
    - Первая форма(MyDelivery):
        - listBox1 - отображает список заказов;
        - textBox1 — показывает общее количество заказов;
        - button1 —  кнопка «Создать новый заказ» открывает форму для создания нового заказа;
        - button2 — кнопка «Создать файл» создает файл для хранения заказов, если он отсутствует;
        - button3 — кнопка «Открыть список заказов» открывает список заказов из файла и отображает их в listBox;
        - button4 — кнопка «Сортировка по району» сортирует заказы по району и отображает их в listBox.

