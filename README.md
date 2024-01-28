# ProfitTest_Cafeteria.API

Проект представляет собой ASP.NET Core Rest API с методами CRUD. Взаимодействие с БД происходит с помощью Entity Framework и InMemoryDB для простой реализации. Между контроллерами и БД есть слой DI контейнеров (репозиториев). Есть JWT авторизация с использованием Cookie. Когда пользователь логинится, токен записывается в Cookie для хранения. При последующих запросах с помощью кастомного Middleware токен извлекается из Cookie и используется в заголовке запроса. Многие запросы Get открытые, а остальные запросы требуют авторизации.

В БД есть две таблицы со связью один-ко-многим (категория еды и еда) и таблица Users. Связь между таблицами FoodCatalog и Food реализована с помощью Fluent API, благодаря чему связанные данные можно получить используя простой LINQ запрос с Include. Там же я добавил небольшое заполнение таблицы.
<p align="center">
  <img style="height: 500px;" src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/7fdc3d24-f295-4ceb-893a-75e977c92fb6" alt="animated" />
</p>

Все методы на изображении. В тело Update и Create нужно пробросить DTO модели, а Read возвращают полные модели. DTO модели конвертируются в Entity с помощью методов расширений из папки Mappers
<p align="center">
  <img style="height: 500px;" src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/e79c17ea-c9c9-4fe3-b52c-a699ceae491f" alt="animated" />
</p>

Есть взаимодействие с Excel и библиотекой EPPlus. Реализовано в Эндпоинте '/api/FoodCatalogs/excel'. Результат будет файл .xlxs с шаблоном данных сверху и далее со всеми категориями еды и ассортиментами внутри этих категорий
<p align="center">
  <img style="height: 500px;" style="width:100%;" src="https://github.com/SuiQRim/ProfitTest_Cafeteria.API/assets/84430915/b8dbf7ad-fe4a-4c7a-99b4-314b69d7eede" alt="animated" />
</p>
