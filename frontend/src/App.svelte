<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Categories from "./lib/components/Categories.svelte";
  import { Router, Link, Route } from "svelte-routing";
  import CreateShoppingList from "./lib/components/CreateShoppingList.svelte";
  import ShoppingList from "./lib/components/ShoppingList.svelte";
  

  onMount(async () => {
  
  });

  onDestroy(() => {   

  });

  export let url = "";
</script>

<h1>Meal Plan</h1>
<Router url="{url}">
<nav>
  <Link to="/categories">Kategorien</Link>
  <Link to="/create-shopping-list">Neue Liste</Link>
</nav>
<main>
    
  <!-- <ShoppingLists />
  <Items />-->
  <Route path="/categories" component={Categories} />  
  <Route path="/create-shopping-list" component={CreateShoppingList} />  
  <Route path="/shopping-list/:id" component={ShoppingList} />  
</main>
</Router>
<style>
</style>
