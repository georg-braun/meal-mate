import { describe, test, assert, it } from "vitest";
import { ShoppingList } from "./ShoppingList";
import { ShoppingListEntry } from "./ShoppingListEntry";

describe("Update list entry", () => {

    test("remove entry from list", () => {
        // arrange
        const shoppingList = ShoppingList.create("1", "Gemüse");
        shoppingList.items.push(ShoppingListEntry.create("I1", "Item1", "300g"))
        const desiredList = structuredClone(shoppingList);
        shoppingList.items.push(ShoppingListEntry.create("I2", "Item1", "400g"))

        // act
        ShoppingList.removeEntry(shoppingList,"I2")

        // assert
        assert.deepEqual(shoppingList, desiredList)
    })

})