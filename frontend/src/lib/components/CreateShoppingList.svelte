<script lang="ts">
  import { Link } from "svelte-routing";
  import apiClient from "../communication/api-client";



let name:string =""
let newListId: string;
</script>

<h1>Create a new shopping list :)</h1>
<input bind:value={name}/>
<div>
    <button on:click={async () => newListId = await apiClient.createShoppingListAsync(name)}>Erstellen</button>
</div>

{#if !!newListId}  
  Eine neue Liste wurde für dich erstellt. Merke dir den Link zu dieser Liste!
  <Link to={`/shopping-list/${newListId}`}>Zur Liste</Link>
{/if}