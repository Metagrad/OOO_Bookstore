-- phpMyAdmin SQL Dump
-- version 4.4.15.7
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1:3306
-- Время создания: Июн 13 2023 г., 09:55
-- Версия сервера: 5.7.13
-- Версия PHP: 5.6.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `Bookstore`
--
CREATE DATABASE IF NOT EXISTS `Bookstore` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `Bookstore`;

-- --------------------------------------------------------

--
-- Структура таблицы `basket`
--

CREATE TABLE IF NOT EXISTS `basket` (
  `order_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `count` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `basket`
--

INSERT INTO `basket` (`order_id`, `book_id`, `count`) VALUES
(31, 3, 3),
(34, 3, 3),
(35, 3, 3),
(36, 3, 3),
(37, 3, 3),
(38, 3, 3),
(39, 3, 3),
(40, 3, 3),
(41, 3, 3),
(42, 3, 3),
(43, 3, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `books`
--

CREATE TABLE IF NOT EXISTS `books` (
  `book_id` int(11) NOT NULL,
  `article_num` varchar(45) DEFAULT NULL,
  `book_name` varchar(80) DEFAULT NULL,
  `book_author` varchar(70) DEFAULT NULL,
  `id_genre` int(11) NOT NULL,
  `book_description` varchar(600) DEFAULT NULL,
  `book_listnum` int(11) DEFAULT NULL,
  `cost` int(11) DEFAULT NULL,
  `book_publisher` int(11) NOT NULL,
  `book_publish_year` year(4) DEFAULT NULL,
  `book_num` int(11) DEFAULT NULL,
  `language_id` int(11) NOT NULL,
  `photo` varchar(100) DEFAULT NULL,
  `size` varchar(15) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `books`
--

INSERT INTO `books` (`book_id`, `article_num`, `book_name`, `book_author`, `id_genre`, `book_description`, `book_listnum`, `cost`, `book_publisher`, `book_publish_year`, `book_num`, `language_id`, `photo`, `size`) VALUES
(2, 'X0W0', 'Война и мир', 'Л.Н.Толстой', 1, 'Всемирно известный роман, посвященный петербургскому обществу', 734, 1234, 1, 2018, 38438, 1, '1.jpg', '24x21x12'),
(3, '90OJ', 'Божественная комедия', 'Данте Алигьери', 15, 'Знаменитая поэма великого итальянского поэта Данте Альгьери, рассказывающая о', 543, 398, 1, 2011, 48535, 1, '2.jpg', '24x21x12'),
(5, '8T1W', 'efeffe', 'efefef', 11, 'eefefef', 324, 243, 1, 2012, 3432, 1, 'picture.png', '24x21x12'),
(6, 'D67U', 'efef', '34344343', 14, 'eefefef', 3434, 243, 5, 2012, 3431, 1, 'picture.png', '24x21x12'),
(7, 'N7ZD', 'rffrf', '343434', 14, 'eefrffr', 3434, 243, 3, 2012, 3432, 1, '1.jpg', '24x21x12'),
(8, 'WIPF', 'rgjorkg', 'rlgprlpgrg', 1, 'rggrgrgrg', 332, 3434, 3, 2019, 3746, 1, 'picture.png', '24x21x12'),
(9, 'XFKW', 'efefef', 'efefefef', 2, 'efefeffefe', 345, 3443, 6, 2012, 4343, 2, 'picture.png', '24x21x12'),
(10, 'OEFE', 'oefopefpfe', 'egrgrght', 2, 'rgrgrg', 434, 545, 4, 2020, 8344, 3, 'picture.png', '24x21x12'),
(11, 'OKFF', 'ofkorkoefo', 'efefkoekf', 2, 'efefefflelfe', 3434, 345, 7, 2021, 3434, 1, 'picture.png', '24x21x12'),
(12, 'XRDG', 'efeffe', 'iefiief', 3, 'effefeffefe', 323, 3434, 1, 2019, 38434, 2, 'picture.png', NULL),
(13, '238T', 'efeffeeffe', 'iefiief', 3, 'effefeffefe', 323, 3434, 1, 2019, 38434, 2, 'picture.png', NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `Genres`
--

CREATE TABLE IF NOT EXISTS `Genres` (
  `id_genre` int(11) NOT NULL,
  `genre_name` varchar(45) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Genres`
--

INSERT INTO `Genres` (`id_genre`, `genre_name`) VALUES
(1, 'Исторический'),
(2, 'Романтика'),
(3, 'Научная фантастика'),
(4, 'Фэнтези'),
(5, 'Зарубежная классическая литература'),
(6, 'Русская классическая литература'),
(7, 'Детские книги'),
(8, 'Современная проза'),
(9, 'Приключения'),
(10, 'Мистика'),
(11, 'Хоррор'),
(12, 'Подростковая литература'),
(14, 'Психология'),
(15, 'Поэзия'),
(16, 'Научная литература'),
(17, 'Комиксы'),
(18, 'Эзотерика'),
(19, 'Искусство'),
(20, 'Религия'),
(21, 'Словари'),
(22, 'Учебная'),
(23, 'Боевики'),
(24, 'Журналистика');

-- --------------------------------------------------------

--
-- Структура таблицы `languages`
--

CREATE TABLE IF NOT EXISTS `languages` (
  `language_id` int(11) NOT NULL,
  `language_name` varchar(45) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `languages`
--

INSERT INTO `languages` (`language_id`, `language_name`) VALUES
(1, 'Русский'),
(2, 'Английский'),
(3, 'Немецкий'),
(4, 'Французский'),
(5, 'Китайский');

-- --------------------------------------------------------

--
-- Структура таблицы `order`
--

CREATE TABLE IF NOT EXISTS `order` (
  `order_id` int(11) NOT NULL,
  `order_date` date DEFAULT NULL,
  `id_user` int(11) NOT NULL,
  `value` int(11) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `discount` decimal(10,4) DEFAULT NULL,
  `finalval` decimal(10,4) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `order`
--

INSERT INTO `order` (`order_id`, `order_date`, `id_user`, `value`, `status`, `discount`, `finalval`) VALUES
(26, '2023-06-12', 1, 884, 'Выполнен', '1.0000', '884.0000'),
(27, '2023-06-12', 1, 796, 'Отменён', '1.0000', '796.0000'),
(34, '2023-06-13', 1, 398, 'Обработка', '1.0000', '398.0000'),
(35, '2023-06-13', 1, 796, 'Обработка', '0.2600', '206.9600'),
(36, '2023-06-13', 1, 796, 'Обработка', '0.2600', '206.9600'),
(37, '2023-06-13', 1, 796, 'Обработка', '0.2600', '206.9600'),
(38, '2023-06-13', 1, 796, 'Обработка', '0.2600', '206.9600'),
(39, '2023-06-13', 1, 1194, 'Обработка', '0.2600', '310.4400'),
(40, '2023-06-13', 1, 1194, 'Обработка', '0.4500', '537.3000'),
(41, '2023-06-13', 1, 1194, 'Обработка', '0.4500', '537.3000'),
(42, '2023-06-13', 1, 1194, 'Обработка', '0.4500', '537.3000'),
(43, '2023-06-13', 1, 796, 'Обработка', '0.2600', '206.9600');

-- --------------------------------------------------------

--
-- Структура таблицы `publishers`
--

CREATE TABLE IF NOT EXISTS `publishers` (
  `id_publisher` int(11) NOT NULL,
  `publisher_name` varchar(45) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `publishers`
--

INSERT INTO `publishers` (`id_publisher`, `publisher_name`) VALUES
(1, 'АСТ'),
(2, 'Владос'),
(3, 'Питер'),
(4, 'КноРу'),
(5, 'Академия'),
(6, 'Весь Мир'),
(7, 'Дрофа'),
(8, 'Эксмо'),
(9, 'Флинта'),
(10, 'Азбука'),
(11, 'Махаон'),
(13, 'Эксмо-Пресс');

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `id_user` int(11) NOT NULL,
  `user_surname` varchar(30) DEFAULT NULL,
  `user_name` varchar(45) DEFAULT NULL,
  `user_patronym` varchar(45) DEFAULT NULL,
  `user_login` varchar(20) DEFAULT NULL,
  `user_password` varchar(100) DEFAULT NULL,
  `user_status` varchar(18) DEFAULT NULL,
  `user_phone` varchar(18) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id_user`, `user_surname`, `user_name`, `user_patronym`, `user_login`, `user_password`, `user_status`, `user_phone`) VALUES
(1, 'Иванов', 'Иван', 'Иванович', 'ivanovii', 'D033E22AE348AEB5660FC2140AEC35850C4DA997', 'Администратор', '+79834883434'),
(2, 'Сидоров', 'Петр', 'Петрович', 'sidorovpp', '12DEA96FEC20593566AB75692C9949596833ADC9', 'Менеджер', '+7(983)488-34-34'),
(3, 'Михайлов', 'Михаил', 'Михайлович', 'mikhailov', '704F2E281EBC6DADA39A6AB27F45DD6834F2082D', 'Менеджер', '+7(984)784-87-43');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `basket`
--
ALTER TABLE `basket`
  ADD KEY `fk_basket_books1_idx` (`book_id`);

--
-- Индексы таблицы `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`book_id`),
  ADD KEY `fk_books_languages1` (`language_id`),
  ADD KEY `fk_books_publishers1_idx` (`book_publisher`),
  ADD KEY `fk_books_Genres1_idx` (`id_genre`);

--
-- Индексы таблицы `Genres`
--
ALTER TABLE `Genres`
  ADD PRIMARY KEY (`id_genre`);

--
-- Индексы таблицы `languages`
--
ALTER TABLE `languages`
  ADD PRIMARY KEY (`language_id`);

--
-- Индексы таблицы `order`
--
ALTER TABLE `order`
  ADD PRIMARY KEY (`order_id`),
  ADD KEY `fk_order_users1_idx` (`id_user`);

--
-- Индексы таблицы `publishers`
--
ALTER TABLE `publishers`
  ADD PRIMARY KEY (`id_publisher`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `books`
--
ALTER TABLE `books`
  MODIFY `book_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT для таблицы `Genres`
--
ALTER TABLE `Genres`
  MODIFY `id_genre` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT для таблицы `languages`
--
ALTER TABLE `languages`
  MODIFY `language_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT для таблицы `order`
--
ALTER TABLE `order`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=44;
--
-- AUTO_INCREMENT для таблицы `publishers`
--
ALTER TABLE `publishers`
  MODIFY `id_publisher` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=15;
--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `basket`
--
ALTER TABLE `basket`
  ADD CONSTRAINT `fk_basket_books1` FOREIGN KEY (`book_id`) REFERENCES `books` (`book_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Ограничения внешнего ключа таблицы `books`
--
ALTER TABLE `books`
  ADD CONSTRAINT `fk_books_Genres1` FOREIGN KEY (`id_genre`) REFERENCES `Genres` (`id_genre`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_books_languages1` FOREIGN KEY (`language_id`) REFERENCES `languages` (`language_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_books_publishers1` FOREIGN KEY (`book_publisher`) REFERENCES `publishers` (`id_publisher`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Ограничения внешнего ключа таблицы `order`
--
ALTER TABLE `order`
  ADD CONSTRAINT `fk_order_users1` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
