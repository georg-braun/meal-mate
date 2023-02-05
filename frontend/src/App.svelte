<script lang="ts">
  import { onMount, onDestroy } from "svelte";

  import Login from "./lib/user/Login.svelte";
  import {
    CategoriesCollectionName,
    currentUser,
    ItemsCollectionName,
    ShoppingListsCollectionName,
  } from "./lib/communication/pocketbase";
  import { ItemRepository } from "./lib/data/ItemRepository";
  import { subscribeToChanges } from "./lib/communication/subscriptions";
  import Categories from "./lib/components/Categories.svelte";
  import { itemRepositoryStore, shoppingListStore } from "./lib/data/stores";
  import Items from "./lib/components/Items.svelte";
  import { Category } from "./lib/domain/item/Category";
  import { Item } from "./lib/domain/item/Item";
  import ShoppingLists from "./lib/components/ShoppingLists.svelte";
  import { ShoppingList } from "./lib/domain/shopping-list/ShoppingList";
    import { get } from "svelte/store";

  const itemRepository = new ItemRepository();

  itemRepositoryStore.set(itemRepository);

  let unsubscribeFuncs: (() => void)[] = [];

  onMount(async () => {
    await itemRepository.loadInitialData(
      CategoriesCollectionName,
      Category.typeName
    );
    await itemRepository.loadInitialData(ItemsCollectionName, Item.typeName);
    await itemRepository.loadShoppingLists();

    unsubscribeFuncs.push(
      await subscribeToChanges(
        Category.typeName,
        CategoriesCollectionName,
        itemRepository
      )
    );
    unsubscribeFuncs.push(
      await subscribeToChanges(
        Item.typeName,
        ItemsCollectionName,
        itemRepository
      )
    );


    // only subscribe to connected shopping lists
    const shoppingListRecordIds = get(shoppingListStore).map(_ => _.id);
    shoppingListRecordIds.forEach(async element => {
      const unsubscribeFunc = await subscribeToChanges(
        ShoppingList.typeName,
        ShoppingListsCollectionName,
        itemRepository,
        element
      )
      unsubscribeFuncs.push(unsubscribeFunc)
    });
    
  });

  onDestroy(() => {
    unsubscribeFuncs.forEach((unsubscribeFunc) => {
      console.log("unsubscribe");
      unsubscribeFunc();
    });
  });
</script>

<main>
  <Login />
  {#if $currentUser}
    <ShoppingLists />
    <Items />
    <Categories />
  {/if}
</main>

<style>
</style>
