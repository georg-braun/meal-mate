<script lang="ts">
  import { onMount, onDestroy } from "svelte";
  import Icon from "@iconify/svelte";
  import Categories from "./lib/components/items/Items.svelte";
  import { Router, Link, Route, navigate } from "svelte-routing";
  import CreateShoppingList from "./lib/components/shopping-list/CreateShoppingList.svelte";
  import ShoppingList from "./lib/components/shopping-list/ShoppingList.svelte";
  import { sendTestMessageAsync } from "./lib/communication/hub/mealMateHub";

  let showItems: boolean;
  let menuIsVisible;

  onMount(async () => {});

  export let url = "";
</script>

<Router {url}>
  <main>
    {#if showItems}
      <Categories />
    {:else}
      <Route path="/create-shopping-list" component={CreateShoppingList} />
      <Route path="/shopping-list/:id" component={ShoppingList} />
    {/if}
  </main>

  {#if menuIsVisible}
    <div class="menu">
      <div>
        <button class="menu_button" on:click={() => {
          showItems = !showItems
          menuIsVisible = false;
        }}
          >Gegenstände {showItems ? "ausblenden" : "einblenden"}</button
        >
      </div>
      <div>
        <button
          class="menu_button"
          on:click={() => {
            showItems = false;
            menuIsVisible = false;
            navigate("/create-shopping-list");
          }}
        >
          Neue Liste erstellen
        </button>
      </div>
    </div>
  {/if}
  <button class="menu-dialog-button" on:click={() => menuIsVisible = !menuIsVisible}>
    <Icon icon="ci:hamburger-md" style="font-size: 30px; " />
  </button>
</Router>

<style>
  .menu-dialog-button {
    position: fixed;
    top: 0px;
    left: 0px;
    margin-right: 40px;
    color: white;
    background-color: #242424;
    border: none;
    height: 50px;
  }

  .menu_button {
    height: 3rem;
    width: 100%;
  }

  .menu {
    position: absolute;
    padding-top: 50px;
    top: 0px;
    left: 0px;
    width: 100%;

    background-color: #242424;
  }
</style>
