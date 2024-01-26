# ProfitTest_Cafeteria.API

Проект представляет собой ASP.NET Core Rest API с методами CRUD. Взаимодействие с БД происходит с помощью Entity Framework и InMemoryDB для простой реализации. Между контроллерами и БД есть слой DI контейнеров.  

В БД есть две таблицы со связью один-ко-многим (категория еды и еда). Связь между таблицами реализована с помощью Fluent API, благодаря чему связанные данные можно получить используя простой LINQ запрос с Include. Там же я добавил небольшое заполнение таблицы.
<p align="center">
  <img src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/5e029660-8b7e-4d2e-a934-9d73f1467576" alt="animated" />
</p>
 
Все методы на изображении. В тело Update и Create нужно пробросить DTO модели, а Read возвращают полные модели. DTO модели конвертируются в Entity с помощью методов расширений из папки Mappers. Для FoodCatalogs достаточно много Read методов. Сделано так, чтобы получать только нужную информацию
<p align="center">
  <img src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/5eaaae8e-4f88-44d0-9f70-b1679588427e" alt="animated" />
</p>

Есть взаимодействие с Excel и библиотекой EPPlus. Реализовано в Эндпоинте '/api/FoodCatalogs/excel'. Результат будет файл .xlxs с шаблоном данных сверху и далее со всеми категориями еды и ассортиментами внутри этих категорий
<p align="center">
  <img style="width:100%;" src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/b8dbf7ad-fe4a-4c7a-99b4-314b69d7eede" alt="animated" />
</p>
