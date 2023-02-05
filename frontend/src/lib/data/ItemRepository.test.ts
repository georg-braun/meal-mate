import { describe, test, assert, it } from "vitest";
import { Category } from "../domain/item/Category";
import { Item } from "../domain/item/Item";
import { ShoppingList } from "../domain/shopping-list/ShoppingList";
import { ShoppingListEntry } from "../domain/shopping-list/ShoppingListEntry";
import { ItemRepository } from "./ItemRepository";
import { itemRepositoryStore } from "./stores";

describe("Item Repository CRUD method", () => {
    test("Adding a category results in added category", () => {
        const repo = new ItemRepository();
        const newCategory: Category = Category.create("1", "Gemüse");
        repo.add(newCategory);

        // assert
        const categories = repo.categories
        assert.containSubset(categories, [newCategory])
    })

    test("Deletion of a category results in deleted category", () => {
        // arrange
        const repo = new ItemRepository();
        const newCategory: Category = Category.create("1", "Gemüse");
        repo.add(newCategory);

        // act
        repo.delete(Category.typeName, "1");

        // assert
        const categories = (repo.categories)
        assert.deepEqual(categories, [])
    })

    test("Adding a item results in added item", () => {
        const repo = new ItemRepository();
        const newItem: Item = Item.create("1", "Apfel", "Gemüse");
        repo.add(newItem);

        // assert
        const items = (repo.items)
        assert.containSubset(items, [newItem])
    })


    test("Deletion of a item results in deleted item", () => {
        // arrange
        const repo = new ItemRepository();
        const newItem: Item = Item.create("1", "Apfel", "Gemüse");
        repo.add(newItem);

        // act
        repo.delete(Item.typeName, "1");

        // assert
        const items = (repo.items)
        assert.deepEqual(items, [])
    })

    test("clear removes all elements", () => {
        // arrange
        const repo = new ItemRepository();

        repo.add(Item.create("1", "Apfel", "Gemüse"));
        repo.add(Item.create("2", "Apfel", "Gemüse"));

        // act
        repo.clear(Item.typeName)

        // assert
        const items = (repo.items)
        assert.deepEqual(items, [])
    })
}
)

describe("Item Repository shopping list", () => {
    test("adding a shopping list results in added shopping list ", () => {
        // arrange
        const repo = new ItemRepository();
        const item = ShoppingList.create("1", "Wocheneinkauf");

        // act
        repo.add(ShoppingList.create("1", "Wocheneinkauf"));

        // assert
        const items = repo.shoppingList
        assert.containSubset(items, [item])
    })

    test("Deletion of a shopping list results in deleted shopping list", () => {
        // arrange
        const repo = new ItemRepository();
        const item = ShoppingList.create("1", "Wocheneinkauf");
        repo.add(item);

        // act
        repo.delete(ShoppingList.typeName, "1")

        // assert
        const items = repo.shoppingList
        assert.containSubset(items, [])
    })

    test("Find the existing shopping list", () => {
        // arrange
        const repo = new ItemRepository();
        const list = ShoppingList.create("1", "Wocheneinkauf");
        repo.add(list);

        // act
        const searchedList = repo.findShoppingList("1")

        // assert
        assert.deepEqual(searchedList, list)
    })

    test("Adding a shopping list entry results in shopping list with that item", () => {
        // arrange
        const repo = new ItemRepository();
        const item = ShoppingList.create("1", "Wocheneinkauf");
        repo.add(ShoppingList.create("1", "Wocheneinkauf"));

        // act
        const entry: ShoppingListEntry = { id: "1", itemId: "i1", qualifier: "2" }
        repo.createEntryOnList("1", entry.itemId, entry.qualifier)

        // assert
        const items = repo.shoppingList
        assert.containSubset(items, [])
    })
})

describe("Item repository get category name", () => {
    test("by category id results in corrent category name", () => {
        const repo = new ItemRepository();
        repo.add(Category.create("1", "Gemüse"));

        const categoryName = repo.getCategoryTitle("1");

        assert.equal(categoryName, "Gemüse")
    })

    test("with undefined category id results in uncategorized text", () => {
        const repo = new ItemRepository();
        repo.add(Category.create("1", "Gemüse"));

        const categoryName = repo.getCategoryTitle(undefined);

        assert.equal(categoryName, "uncategorized")
    })

    test("with empty category id results in uncategorized text", () => {
        const repo = new ItemRepository();
        repo.add(Category.create("1", "Gemüse"));

        const categoryName = repo.getCategoryTitle(undefined);

        assert.equal(categoryName, "uncategorized")
    })

    test("with non existing category results in a hint", () => {
        const repo = new ItemRepository();
        repo.add(Category.create("1", "Gemüse"));

        const categoryName = repo.getCategoryTitle("2");

        assert.equal(categoryName, "Category id: 2")
    })

    test("get the category of a shopping list entry", () => {
        // arrange
        const repo = new ItemRepository();
        repo.add(Category.create("Cat1", "Gemüse"))
        repo.add(Item.create("Item1", "Paprika", "Cat1"))
        const list = ShoppingList.create("List1", "Wocheneinkauf");
        list.items.push(ShoppingListEntry.create("Entry1", "Item1", "1x"))
        repo.add(list);

        // act
        const category = repo.getCategoryTitleOfShoppingListEntry("List1", "Entry1")
  
        // assert
        assert.equal(category, "Gemüse")
    })
})

describe("Item repository get item name", () => {
    test("by item id ", () => {
        const repo = new ItemRepository();
        repo.add(Item.create("1", "Apfelringe", "Gemüse"));

        const itemTitle = repo.getItemTitle("1");

        assert.equal(itemTitle, "Apfelringe")
    })
})

describe("Update of item", () => {

        test("results in correct updated item", () => {
            // arrange
            const repo = new ItemRepository();
            repo.add(Item.create("1", "Apfelringe", "Gemüse"));
    
            // act
            repo.update(Item.create("1", "Getrocknete Bananen", "Gemüse"))

            // assert
            const item = repo.find<Item>(Item.typeName, "1");
            assert.equal(item.title, "Getrocknete Bananen")
        })
    
})