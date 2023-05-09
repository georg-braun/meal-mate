<script lang="ts">
  import Icon from "@iconify/svelte";
  import Categories from "./lib/components/items/Items.svelte";
  import { Router, Link, Route, navigate } from "svelte-routing";
  import CreateShoppingList from "./lib/components/shopping-list/CreateShoppingList.svelte";
  import ShoppingList from "./lib/components/shopping-list/ShoppingList.svelte";
  import CreateTemplate from "./lib/components/template/CreateTemplate.svelte";
  import TemplateOverview from "./lib/components/template/TemplateOverview.svelte";
  import ExistingTemplate from "./lib/components/template/ExistingTemplate.svelte";

  let showItems: boolean;
  let menuIsVisible;

  export let url = "";
</script>

<Router {url}>
  <main>
    <div class="header">
      <button
        class="menu-dialog-button"
        on:click={() => (menuIsVisible = !menuIsVisible)}
      >
        <Icon icon="ci:hamburger-md" style="font-size: 30px; " />
      </button>
      <div class="header__text">Meal Mate</div>
    </div>

    {#if !menuIsVisible}
      {#if showItems}
        <Categories />
      {:else}
        <Route path="/create-shopping-list" component={CreateShoppingList} />
        <Route path="/shopping-list/:id" component={ShoppingList} />
        <Route path="/templates" component={TemplateOverview} />
        <Route path="/template/:id" component={ExistingTemplate} />
        <Route path="/create-template" component={CreateTemplate} />
      {/if}
    {/if}
  </main>

  {#if menuIsVisible}
    <div class="menu">
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

      <div>
        <button
          class="menu_button"
          on:click={() => {
            showItems = false;
            menuIsVisible = false;
            navigate("/templates");
          }}
        >
          Rezepte
        </button>
      </div>

      <div>
        <button
          class="menu_button"
          on:click={() => {
            showItems = !showItems;
            menuIsVisible = false;
          }}>Gegenstände {showItems ? "ausblenden" : "einblenden"}</button
        >
      </div>

    </div>
  {/if}
</Router>

<style>
  .menu-dialog-button {
    top: 0px;
    left: 0px;
    margin-right: 10px;
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
    background-color: #242424;
  }

  .header {
    display: flex;
    flex-direction: row;
    align-items: center;
    background-color: #242424;
    height: 50px;
  }
  .header__text {
    font-size: 30px;
    color: white;
  
  }
</style>
