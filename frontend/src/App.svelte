<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Categories from "./lib/components/Categories.svelte";
  import { Router, Link, Route } from "svelte-routing";
  import CreateShoppingList from "./lib/components/CreateShoppingList.svelte";
  import ShoppingList from "./lib/components/ShoppingList.svelte";

  
  let showCategoriesOverlay : boolean;

  onMount(async () => {
  
  });

  onDestroy(() => {   

  });

  export let url = "";
</script>

<h1>Meal Plan</h1>
<button on:click={() => showCategoriesOverlay = !showCategoriesOverlay}>Kategorien {showCategoriesOverlay ? "schließen" : "bearbeiten"}</button>
<Router url="{url}">
  {#if !showCategoriesOverlay}  
<nav>  
  <Link to="/create-shopping-list">Neue Liste erstellen</Link>
</nav>
{/if}
<main>
      
  {#if showCategoriesOverlay}
    <Categories />
  {:else}
  <!-- <Route path="/categories" component={Categories} />   -->
  <Route path="/create-shopping-list" component={CreateShoppingList} />  
  <Route path="/shopping-list/:id" component={ShoppingList} />  

  {/if}
  
</main>
</Router>
<style>
</style>
