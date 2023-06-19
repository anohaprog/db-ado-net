using Npgsql;
//Create DB with ADO.NET

// Создание таблиц
//CreateMainCategoriesTable();
//CreateCategoriesTable();
//CreateSubCategoriesTable();

// Вставка
//InsertMainCategories();
//InsertCategories();
//InsertSubCategories();

// Выборка
SelectMainCategory();
SelectCategory();
SelectSubCategory();

// Добавление в любую таблицу
AddToTable();

const string connectionString = "Host=localhost;Username=root;Password=root;Database=avito";

static void CreateMainCategoriesTable()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sqlMainCategories = @"
CREATE SEQUENCE maincategories_id_seq;

CREATE TABLE maincategories
(
    id      BIGINT                  NOT NULL   DEFAULT NEXTVAL('maincategories_id_seq'),
    name    CHARACTER VARYING(255)   NOT NULL,

    CONSTRAINT maincategories_pkey PRIMARY KEY (id),
    CONSTRAINT maincategories_name_unique UNIQUE (name)
);
";
    using var cmdMC = new NpgsqlCommand(sqlMainCategories, connection);
    var actionMainCategories = cmdMC.ExecuteNonQuery().ToString();
    Console.WriteLine("Created MAINCATEGORIES table. Return " + actionMainCategories);
}
static void CreateCategoriesTable()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sqlCategories = @"
CREATE SEQUENCE categories_id_seq;

CREATE TABLE categories
(
    id                  BIGINT                  NOT NULL    DEFAULT NEXTVAL('categories_id_seq'),
    maincategories_id   BIGINT                  NOT NULL,
    name                CHARACTER VARYING(255)  NOT NULL,    

    CONSTRAINT categories_pkey PRIMARY KEY (id),
    CONSTRAINT categories_fk_maincategories_id FOREIGN KEY (maincategories_id) REFERENCES maincategories(id)
);
";
    using var cmdC = new NpgsqlCommand(sqlCategories, connection);
    var actionCategories = cmdC.ExecuteNonQuery().ToString();
    Console.WriteLine("Created CATEGORIES table. Return " + actionCategories);
}

static void CreateSubCategoriesTable()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sqlSubCategories = @"
CREATE SEQUENCE subcategories_id_seq;

CREATE TABLE subcategories
(
    id              BIGINT                  NOT NULL    DEFAULT NEXTVAL('subcategories_id_seq'),
    categories_id   BIGINT                  NOT NULL,
    name            CHARACTER VARYING(255)  NOT NULL,    

    CONSTRAINT subcategories_pkey PRIMARY KEY (id),
    CONSTRAINT subcategories_fk_categories_id FOREIGN KEY (categories_id) REFERENCES categories(id) ON DELETE CASCADE
);
";
    using var cmdSC = new NpgsqlCommand(sqlSubCategories, connection);
    var actionSubCategories = cmdSC.ExecuteNonQuery().ToString();
    Console.WriteLine("Created SUBCATEGORIES table. Return " + actionSubCategories);
}

static void InsertMainCategories()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO maincategories(name)
VALUES (:name)
";

    string[] mainCategories = {"Транспорт", "Недвижимость", "Работа", "Услуги", "Личные вещи"};

    foreach(string category in mainCategories)
    {
        using var cmd = new NpgsqlCommand(sql, connection);
        var parameters = cmd.Parameters;
        parameters.Add(new NpgsqlParameter("name", category));

        var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
        Console.WriteLine($"Insert into MAINCATEGORIES table. Result: {affectedRowsCount}");
    }
}

static void InsertCategories()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO categories(maincategories_id, name)
VALUES (:maincategories_id, :name)
";

    using var cmd = new NpgsqlCommand(sql, connection);
    var parameters = cmd.Parameters;
    parameters.Add(new NpgsqlParameter("maincategories_id", 1));
    parameters.Add(new NpgsqlParameter("name", "Автомобили"));

    var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount}");

    using var cmd2 = new NpgsqlCommand(sql, connection);
    var parameters2 = cmd2.Parameters;
    parameters2.Add(new NpgsqlParameter("maincategories_id", 1));
    parameters2.Add(new NpgsqlParameter("name", "Мотоциклы"));

    var affectedRowsCount2 = cmd2.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount2}");

    using var cmd3 = new NpgsqlCommand(sql, connection);
    var parameters3 = cmd3.Parameters;
    parameters3.Add(new NpgsqlParameter("maincategories_id", 2));
    parameters3.Add(new NpgsqlParameter("name", "Купить"));

    var affectedRowsCount3 = cmd3.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount3}");

    using var cmd4 = new NpgsqlCommand(sql, connection);
    var parameters4 = cmd4.Parameters;
    parameters4.Add(new NpgsqlParameter("maincategories_id", 2));
    parameters4.Add(new NpgsqlParameter("name", "Снять посуточно"));

    var affectedRowsCount4 = cmd4.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount4}");

    using var cmd5 = new NpgsqlCommand(sql, connection);
    var parameters5 = cmd5.Parameters;
    parameters5.Add(new NpgsqlParameter("maincategories_id", 2));
    parameters5.Add(new NpgsqlParameter("name", "Снять долгосрочно"));

    var affectedRowsCount5 = cmd5.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount5}");
}

static void InsertSubCategories()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO subcategories(categories_id, name)
VALUES (:categories_id, :name)
";

    using var cmd = new NpgsqlCommand(sql, connection);
    var parameters = cmd.Parameters;
    parameters.Add(new NpgsqlParameter("categories_id", 2));
    parameters.Add(new NpgsqlParameter("name", "Новостройки"));

    var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount}");

    using var cmd2 = new NpgsqlCommand(sql, connection);
    var parameters2 = cmd2.Parameters;
    parameters2.Add(new NpgsqlParameter("categories_id", 2));
    parameters2.Add(new NpgsqlParameter("name", "Вторичка"));

    var affectedRowsCount2 = cmd2.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount2}");

    using var cmd3 = new NpgsqlCommand(sql, connection);
    var parameters3 = cmd3.Parameters;
    parameters3.Add(new NpgsqlParameter("categories_id", 2));
    parameters3.Add(new NpgsqlParameter("name", "Комнаты"));

    var affectedRowsCount3 = cmd3.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount3}");

    using var cmd4 = new NpgsqlCommand(sql, connection);
    var parameters4 = cmd4.Parameters;
    parameters4.Add(new NpgsqlParameter("categories_id", 2));
    parameters4.Add(new NpgsqlParameter("name", "Дома, дачи, коттеджи"));

    var affectedRowsCount4 = cmd4.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount4}");

    using var cmd5 = new NpgsqlCommand(sql, connection);
    var parameters5 = cmd5.Parameters;
    parameters5.Add(new NpgsqlParameter("categories_id", 2));
    parameters5.Add(new NpgsqlParameter("name", "Гаражи"));

    var affectedRowsCount5 = cmd5.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount5}");
}

static void SelectMainCategory()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
SELECT id, name FROM maincategories
";
    using var cmd = new NpgsqlCommand(sql, connection);

    var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        var id = reader.GetInt64(0);
        var name = reader.GetString(1);

        Console.WriteLine($"Main categories: {id}. {name}");
    }
}

static void SelectCategory()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
SELECT m.name main_name, c.name FROM maincategories m
JOIN categories c
    ON m.id = c.maincategories_id
";
    using var cmd = new NpgsqlCommand(sql, connection);

    var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        var main_name = reader.GetString(0);
        var name = reader.GetString(1);

        Console.WriteLine($"Categories: {main_name} - {name}");
    }
}

static void SelectSubCategory()
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
SELECT m.name main_name, c.name, s.name sub_name FROM maincategories m
JOIN categories c
    ON m.id = c.maincategories_id
JOIN subcategories s
    ON c.id = s.categories_id
";
    using var cmd = new NpgsqlCommand(sql, connection);

    var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        var main_name = reader.GetString(0);
        var name = reader.GetString(1);
        var sub_name = reader.GetString(2);

        Console.WriteLine($"SubCategories: {main_name} - {name} - {sub_name}");
    }
}

static void AddToTable()
{
    Console.Write("Выберите таблицу: \n 1 MainCategories \n 2 Categories \n 3 SubCategories \n");
    int num = Convert.ToInt32(Console.ReadLine());
    if (num == 1)
    {
        Console.WriteLine("Введите наименование главной категории:");
        string main_name = Console.ReadLine();
        InsertMainCategories2(main_name);
    }
    else if (num == 2)
    {
        Console.WriteLine("Введите наименование id главной категории:");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите наименование категории:");
        string name = Console.ReadLine();
        InsertCategories2(id, name);
    }
    else if (num == 3)
    {
        Console.WriteLine("Введите наименование id категории:");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите наименование подкатегории:");
        string sub_name = Console.ReadLine();
        InsertCategories2(id, sub_name);
    }
}

static void InsertMainCategories2(string name)
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO maincategories(name)
VALUES (:name)
";
    using var cmd = new NpgsqlCommand(sql, connection);
    var parameters = cmd.Parameters;
    parameters.Add(new NpgsqlParameter("name", name));

    var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into MAINCATEGORIES table. Result: {affectedRowsCount}");
}

static void InsertCategories2(int maincategories_id, string name)
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO categories(maincategories_id, name)
VALUES (:maincategories_id, :name)
";

    using var cmd = new NpgsqlCommand(sql, connection);
    var parameters = cmd.Parameters;
    parameters.Add(new NpgsqlParameter("maincategories_id", maincategories_id));
    parameters.Add(new NpgsqlParameter("name", name));

    var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into CATEGORIES table. Result: {affectedRowsCount}");
}

static void InsertSubCategories2(int categories_id, string sub_name)
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var sql = @"
INSERT INTO subcategories(categories_id, name)
VALUES (:categories_id, :name)
";

    using var cmd = new NpgsqlCommand(sql, connection);
    var parameters = cmd.Parameters;
    parameters.Add(new NpgsqlParameter("categories_id", categories_id));
    parameters.Add(new NpgsqlParameter("name", sub_name));

    var affectedRowsCount = cmd.ExecuteNonQuery().ToString();
    Console.WriteLine($"Insert into SUBCATEGORIES table. Result: {affectedRowsCount}");
}