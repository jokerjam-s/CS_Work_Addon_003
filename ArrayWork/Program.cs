using System;
/*
Написать 2 функции для работы с массивом: 
    AddToArray И RemoveFromArray – первая добавляет к числовому массиву значение, тем самым увеличивая массив, 
    а вторая удаляет элемент под нужным индексом и уменьшает массив на 1.

Написать функцию Shuffle, которая перемешивает элементы массива в случайном порядке.

Написать программу со следующими командами: 
    SetNumbers – пользователь вводит числа через пробел, а программа запоминает их в массив
    AddNumbers – пользователь вводит числа, которые добавятся к уже существующему массиву 
    RemoveNumbers - пользователь вводит числа, которые если найдутся в массиве, то будут удалены 
    Numbers – ввывод текущего массива 
    Sum – найдется сумма всех элементов чисел
*/

/// код символа цифры 0 для перевода строкового символа в число
const int nilCharCode = 48;

/// Добавлениеи в массив нового элемента. Возвращает массив с новым элементом.
///     array - изменяемый массив
///     value - добавляемое значение
int[] AddToArray(int[] array, int value)
{
    int[] newArray = new int[array.Length + 1];
    for (int i = 0; i < array.Length; i++)
    {
        newArray[i] = array[i];
    }

    newArray[newArray.Length - 1] = value;

    return newArray;
}

/// Удалениеи элементва из массива. Возвращает массив без указанного элемента.
///     array - массив для удаления
///     itemIndex - индекс удаляемого элемента
/// Если itemIndex за пределами границ массива возаращает исходный массив.
int[] RemoveFromArray(int[] array, int itemIndex)
{
    if (itemIndex < 0 || itemIndex >= array.Length)
        return array;

    int[] newArray = new int[array.Length - 1];

    for (int i = 0; i < itemIndex; i++)
    {
        newArray[i] = array[i];
    }
    for (int i = itemIndex; i < newArray.Length; i++)
    {
        newArray[i] = array[i + 1];
    }

    return newArray;
}

/// Перемешивает массив в случайном порядке. Возвращает перемешанный масив.
///     shuffleCount - количество перестановок в массиве, 
///                    если не указано или <=0 выполнится aaray.Length/2 раз 
int[] Shuffle(int[] array, int shuffleCount = 0)
{
    if (shuffleCount <= 0)
        shuffleCount = array.Length / 2;

    Random random = new Random();
    for (int i = 0; i < shuffleCount; i++)
    {
        int firstPos = random.Next(array.Length);
        int secondPos = random.Next(array.Length);

        int tmp = array[firstPos];
        array[firstPos] = array[secondPos];
        array[secondPos] = tmp;
    }

    return array;
}

/// Получение строки данных от пользователя.
///     message - сообщение пользователю
string InputStr(string message)
{
    Console.Write(message);
    return Console.ReadLine();
}


/// переводит строку цифр в массив
///     strLine - цифровая строка для преобразования
int[] StringParse(string strLine)
{
    int[] array = { };
    bool foundedDigit = false;
    int number = 0;
    int sign = 1;

    for (int i = 0; i < strLine.Length; i++)
    {
        if (strLine[i] >= '0' && strLine[i] <= '9')
        {
            number = number * 10 + strLine[i] - nilCharCode;
            foundedDigit = true;
        }
        else if (strLine[i] == '-')
        {
            sign = -1;
        }
        else
        {
            if (foundedDigit)
            {
                array = AddToArray(array, number * sign);
                number = 0;
            }
            foundedDigit = false;
            sign = 1;
        }
    }

    if (foundedDigit)   // доп. проверка, иначе не вносит последнее число. Не изящно однако
    {
        array = AddToArray(array, number * sign);
    }

    return array;
}

/// Запрашивает строку чисел, возвра щает массив сформированный на основе полученных данных.
int[] SetNumbers()
{
    return StringParse(InputStr("Input line of numbers: "));
}

/// Пользователь вводит числа, которые добавятся к уже существующему массиву.
/// Возвращает новый массив с добавленными элементами.
///     array - исходный массив для добавления 
int[] AddNumbers(int[] array)
{
    int[] numbersToAdd = StringParse(InputStr("Input numbers for add: "));

    for (int i = 0; i < numbersToAdd.Length; i++)
    {
        array = AddToArray(array, numbersToAdd[i]);
    }

    return array;
}


/// Пользователь вводит числа, которые если найдутся в массиве, то будут удалены/
/// Возвращает массив с удаленной информацией.
///     array - массив для поиска и удаления информации 
int[] RemoveNumbers(int[] array)
{
    int[] numbersToDel = StringParse(InputStr("Input numbers for delete: "));

    for (int i = 0; i < numbersToDel.Length; i++)
    {
        for (int j = array.Length - 1; j >= 0; j--)
        {
            if (numbersToDel[i] == array[j])
            {
                array = RemoveFromArray(array, j);
            }

        }
    }

    return array;
}


/// Формирование строки для ввывода массива на экран.
///     array - массив для вывода
string Numbers(int[] array)
{
    string result = string.Empty;

    if (array.Length == 0)
    {
        result = "Array is empty.";
    }
    else
    {
        for (int i = 0; i < array.Length; i++)
        {
            result += $"{array[i]} ";
        }
    }
    return result;
}


/// Возвращает сумму элементов массива.
///     array - массив для расчета суммы
long Sum(int[] array)
{
    long result = 0;

    for (int i = 0; i < array.Length; i++)
    {
        result += array[i];
    }

    return result;
}


/// Вывод справки о системе
void PrintHelp()
{
    Console.WriteLine("System help ");
    Console.WriteLine("\thelp    - print program help");
    Console.WriteLine("\tnew     - input new array");
    Console.WriteLine("\tadd     - append numbers to array");
    Console.WriteLine("\tremove  - remove numbers from array");
    Console.WriteLine("\tsum     - calculate sum array");
    Console.WriteLine("\tshuf    - shuffle array");
    Console.WriteLine("\tprint   - print array");
    Console.WriteLine("\texit    - close program");
}

/// MAin body.
Console.Clear();

int[] array = { };

while (true)
{
    string command = InputStr("> ").ToLower();

    if (command == "help")
        PrintHelp();
    else if (command == "new")
        array = SetNumbers();
    else if (command == "add")
        array = AddNumbers(array);
    else if (command == "remove")
        array = RemoveNumbers(array);
    else if (command == "sum")
        Console.WriteLine($"Total sum {Sum(array)}");
    else if (command == "shuf")
        array = Shuffle(array);
    else if (command == "print")
        Console.WriteLine(Numbers(array));
    else if (command == "exit")
        break;
    else
        Console.WriteLine("Wrong command. Use command 'help' for showing system help");
}

