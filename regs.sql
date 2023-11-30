USE Resto
GO

--BASE REGS
INSERT INTO [Role]
(roleName, roleDescription)
VALUES
('manager', 'manager access')
GO

INSERT INTO [Role]
(roleName, roleDescription)
VALUES
('waiter', 'waiter access')
GO
--
EXEC insEmployee 'E11111111', 'Manager', 'Manager', 1
EXEC insEmployee 'E22222222', 'Waiter', 'Waiter', 2
EXEC insEmployee 'E33333333', 'John', 'Doe', 2
EXEC insEmployee 'E44444444', 'Jane', 'Smith', 2
EXEC insEmployee 'E55555555', 'Michael', 'Johnson', 2
EXEC insEmployee 'E66666666', 'Emily', 'Williams', 2
EXEC insEmployee 'E77777777', 'Daniel', 'Brown', 2
EXEC insEmployee 'E88888888', 'Olivia', 'Jones', 2
EXEC insEmployee 'E99999999', 'David', 'Garcia', 2
EXEC insEmployee 'E10000000', 'Sophia', 'Miller', 2
EXEC insEmployee 'E45146417', 'Thomas', 'Pufahl', 1

EXEC insProductCategory 'Non-Vegan Plate'
EXEC insProductCategory 'Vegan Plate'
EXEC insProductCategory 'Vegetarian Plate'
EXEC insProductCategory 'Non-Alcoholic Beverage'
EXEC insProductCategory 'Alcoholic Beverage'

-- Non-Vegan
EXEC insProduct 'Grilled Steak with Garlic Butter', 1, 20, 10, 25.99, 'Juicy grilled steak topped with savory garlic butter, served with a side of roasted vegetables and mashed potatoes.'
EXEC insProduct 'Spaghetti Carbonara', 1, 20, 10, 14.99, 'Classic Italian pasta dish with creamy egg and cheese sauce, pancetta, and black pepper.'
EXEC insProduct 'Salmon with Lemon Dill Sauce', 1, 20, 10, 18.99, 'Pan-seared salmon fillet drizzled with a refreshing lemon dill sauce, served with steamed asparagus and rice.'
EXEC insProduct 'Cheese and Ham Omelette', 1, 20, 10, 9.99, 'Fluffy omelette filled with melted cheese, diced ham, and fresh herbs, served with crispy hash browns.'
EXEC insProduct 'Beef and Broccoli Stir-Fry', 1, 20, 10, 16.99, 'Tender strips of beef and crisp broccoli florets stir-fried in a savory soy-ginger sauce, served over steamed rice.'
-- Vegan Plate
EXEC insProduct 'Vegan Buddha Bowl', 2, 20, 10, 14.99, 'A nourishing bowl filled with a variety of colorful vegetables, tofu, and quinoa, topped with a tahini dressing.'
EXEC insProduct 'Chickpea and Spinach Curry', 2, 20, 10, 12.99, 'A flavorful blend of chickpeas, spinach, and aromatic spices in a creamy coconut milk curry sauce, served over rice.'
EXEC insProduct 'Vegan Lentil Soup', 2, 20, 10, 8.99, 'Hearty lentil soup with a medley of vegetables, simmered in a savory vegetable broth.'
EXEC insProduct 'Vegan Pad Thai', 2, 20, 10, 13.99, 'Stir-fried rice noodles with tofu, bean sprouts, and peanuts in a tangy tamarind sauce.'
EXEC insProduct 'Roasted Vegetable and Quinoa Salad', 2, 20, 10, 10.99, 'A colorful salad featuring roasted vegetables, fluffy quinoa, and a zesty vinaigrette dressing.'
-- Vegetarian Plate
EXEC insProduct 'Vegetarian Caprese Salad', 3, 20, 10, 9.99, 'A fresh salad featuring ripe tomatoes, mozzarella cheese, basil leaves, and balsamic glaze.'
EXEC insProduct 'Spinach and Feta Stuffed Mushrooms', 3, 20, 10, 10.99, 'Mushrooms filled with a savory mixture of spinach, feta cheese, and herbs, baked to perfection.'
EXEC insProduct 'Vegetarian Tofu Stir-Fry', 3, 20, 10, 12.99, 'Tofu cubes sautéed with colorful vegetables in a flavorful ginger-soy sauce, served over rice.'
EXEC insProduct 'Vegetarian Enchiladas', 3, 20, 10, 11.99, 'Corn tortillas filled with a blend of black beans, corn, and cheese, topped with enchilada sauce and baked.'
EXEC insProduct 'Eggplant Parmesan', 3, 20, 10, 13.99, 'Slices of eggplant breaded, fried, and layered with marinara sauce and melted cheese.'
-- Non-Alcoholic
EXEC insProduct 'Freshly Squeezed Orange Juice', 4, 20, 10, 4.99, 'Freshly squeezed oranges for a refreshing and vitamin-packed beverage.'
EXEC insProduct 'Iced Green Tea', 4, 20, 10, 3.99, 'Chilled green tea, lightly sweetened and garnished with a slice of lemon.'
EXEC insProduct 'Virgin Mojito', 4, 20, 10, 5.99, 'A refreshing mix of lime, mint, and soda water, perfect for a zesty twist on a classic drink.'
EXEC insProduct 'Sparkling Lemonade', 4, 20, 10, 4.49, 'Bubbly lemonade with a hint of sweetness, served over ice.'
EXEC insProduct 'Ginger Kombucha', 4, 20, 10, 6.99, 'A tangy and probiotic-rich fermented tea infused with ginger for a zingy kick.'
-- Alcoholic
EXEC insProduct 'Classic Martini', 5, 20, 10, 9.99, 'A timeless cocktail made with gin or vodka and a hint of vermouth, garnished with an olive or lemon twist.'
EXEC insProduct 'Margarita on the Rocks', 5, 20, 10, 8.99, 'A refreshing blend of tequila, triple sec, lime juice, and agave syrup, served over ice with a salted rim.'
EXEC insProduct 'Red Wine (Cabernet Sauvignon)', 5, 20, 10, 12.99, 'A robust and full-bodied red wine with rich flavors of dark fruits and a velvety finish.'
EXEC insProduct 'Old Fashioned', 5, 20, 10, 10.99, 'A classic cocktail made with bourbon, sugar, bitters, and a twist of citrus peel.'
EXEC insProduct 'Beer Flight (Assorted Craft Beers)', 5, 20, 10, 14.99, 'A selection of four different craft beers, served in smaller portions for tasting and pairing.'

-- Order Status
-- pre-cook
EXEC insOrderStatus 100, 'InQueue'
EXEC insOrderStatus 101, 'InProgress'
-- post-cook
EXEC insOrderStatus 200, 'Ready'
EXEC insOrderStatus 201, 'Delivered'
EXEC insOrderStatus 202, 'Payable'
EXEC insOrderStatus 203, 'Paid'
EXEC insOrderStatus 204, 'Closed'
-- post-cook client errors
EXEC insOrderStatus 300, 'Returned'

-- RESTO TABLES
EXEC insRestoTable 1
EXEC insRestoTable 2
EXEC insRestoTable 3
EXEC insRestoTable 4
EXEC insRestoTable 5
EXEC insRestoTable 6

--- ORDERS 
EXEC insOrder 3, 1, 1
EXEC insOrder 4, 2, 1

-- Order item
EXEC insOrderItem 1000, 1, 1
EXEC insOrderItem 1000, 2, 1
EXEC insOrderItem 1000, 3, 1

EXEC insOrderItem 1001, 4, 1
EXEC insOrderItem 1001, 5, 1
EXEC insOrderItem 1001, 6, 1

-- ins orderNumber in RestoTable
EXEC updRestoTable 1, 1000
EXEC updRestoTable 2, 1001