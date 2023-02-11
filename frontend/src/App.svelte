<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Categories from "./lib/components/Categories.svelte";

  onMount(async () => {
  
  });

  onDestroy(() => {   

  });
</script>

<main>
  <h1>Meal Plan</h1>
  <!-- <ShoppingLists />
  <Items />-->
  <Categories /> 
</main>

<style>
</style>
