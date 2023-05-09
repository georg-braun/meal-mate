<script lang="ts">
    import { onMount } from "svelte";
    import { navigate } from "svelte-routing";
    import apiClient from "../../communication/api/api-client";

    let templates: Template[] = [];
    onMount(async () => {
        console.log("mounted");
        templates = await apiClient.getTemplatesAsync();
    });

    function createNewTemplate() {
        navigate("/create-template");
    }
</script>

<button on:click={createNewTemplate}>Neues Rezept</button>

{#each templates as template}
    <div>{template.name}</div>
    <button
        class="menu_button"
        on:click={() => {
            navigate(`/template/${template.id}`);
        }}
    >
       Edit
    </button>
{/each}
