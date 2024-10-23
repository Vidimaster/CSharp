Консольное приложение запускается в Microsoft Visual Studio командой:

dotnet run _cityDistrict "" _firstDeliveryDateTim "" _deliveryOrder "" _deliveryLog ""

 где:
 _cityDistrict --параметр района доставки;
 _firstDeliveryDateTime --время первой доставки в формате yyyy-MM-dd HH:mm:ss;
 _deliveryOrder --путь к файлу с резульатом, иначе файл сохраняется в папке проекта;
 _deliveryLog --путь к файлу с логом, иначе файл сохраняется в папке проекта;

например, команда выводит результат в файл output.txt и делает запись в логе log.txt в папке проекта:
dotnet run _cityDistrict "N3" _firstDeliveryDateTime "2024-09-29 16:10:21"

входные данные находятся в файле input.json в папке проекта

