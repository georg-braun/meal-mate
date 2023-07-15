<script lang="ts">
  import Icon from "@iconify/svelte";
  import Items from "@/features/ItemsOverview/components/Items.svelte";
  import { Router, Link, Route, navigate } from "svelte-routing";
  import CreateShoppingList from "./features/CreateNewShoppingList/components/CreateShoppingList.svelte";
  import ShoppingList from "@/features/ShoppingList/components/ShoppingList.svelte";
  import CreateTemplate from "@/features/CreateTemplate/components/CreateTemplate.svelte";
  import TemplateOverview from "@/features/TemplatesOverview/components/TemplateOverview.svelte";
  import ExistingTemplate from "@/features/EditTemplate/components/EditTemplate.svelte";

  export let url = "";

  const buttonStyle = "underline m-2"
</script>
<Router {url}>
  <main>
    <div class="flex flex-row gap-6 justify-center items-center h-16 bg-slate-700 text-slate-100">
      <div>
        <button
          class="{buttonStyle}"
          on:click={() => {
            navigate("/create-shopping-list");
          }}
        >
          Liste erstellen
        </button>
      </div>

      <div>
        <button
          class="{buttonStyle}"
          on:click={() => {
            navigate("/templates");
          }}
        >
          Rezepte
        </button>
      </div>

      <div>
        <button
          class="{buttonStyle}"
          on:click={() => {
            navigate("/items");
          }}>Gegenstände</button
        >
      </div>

    </div>

   <div class="container mt-8 mx-auto">

        <Route path="/create-shopping-list" component={CreateShoppingList} />
        <Route path="/shopping-list/:id" component={ShoppingList} />
        <Route path="/templates" component={TemplateOverview} />
        <Route path="/template/:id" component={ExistingTemplate} />
        <Route path="/create-template" component={CreateTemplate} />
        <Route path="/items" component={Items} />

    </div>

  </main>




    
 
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





  .header {
    display: flex;
    flex-direction: row;
    align-items: center;

    height: 50px;
  }
  .header__text {
    font-size: 30px;
    color: white;
  
  }
</style>
