<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Categories from "./lib/components/items/Items.svelte";
  import { Router, Link, Route, navigate } from "svelte-routing";
  import CreateShoppingList from "./lib/components/shopping-list/CreateShoppingList.svelte";
  import ShoppingList from "./lib/components/shopping-list/ShoppingList.svelte";
  import { sendTestMessageAsync } from "./lib/communication/mealMateHub";

  let showItems: boolean;

  onMount(async () => {});

  export let url = "";
</script>

<Router {url}>
  <nav>
    <button on:click={() => (showItems = !showItems)}
      >Gegenstände {showItems ? "ausblenden" : "einblenden"}</button
    >
    <button
      on:click={() => {
        showItems = false;
        navigate("/create-shopping-list");
      }}
    >
      Neue Liste erstellen
    </button>
  </nav>

  <main>
    {#if showItems}
      <Categories />
    {:else}
      <Route path="/create-shopping-list" component={CreateShoppingList} />
      <Route path="/shopping-list/:id" component={ShoppingList} />
    {/if}
  </main>
</Router>

<style>
</style>
