<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Categories from "./lib/components/Categories.svelte";
  import { Router, Link, Route } from "svelte-routing";

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
</nav>
<main>
    
  <!-- <ShoppingLists />
  <Items />-->
  <Route path="/categories" component={Categories} />  
</main>
</Router>
<style>
</style>
