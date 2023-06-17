<script lang="ts">
    import { onMount } from "svelte";
    import { navigate } from "svelte-routing";
    import apiClient from "../../communication/api/api-client";
    import ActionButton from "../ActionButton.svelte";

    let templates: Template[] = [];
    onMount(async () => {
        console.log("mounted");
        templates = await apiClient.getTemplatesAsync();
    });

    function createNewTemplate() {
        navigate("/create-template");
    }
</script>


<div class="flex justify-center gap-8">


{#each templates as template}
    <div class=" border-2 p-8 hover:border-black">
        <div class="text-2xl">{template.name}</div>
        <button
            on:click={() => {
                navigate(`/template/${template.id}`);
            }}
        >
 

            Bearbeiten
    
        </button>
    </div>
{/each}
</div>

<div class="flex justify-center mt-10">

    <ActionButton background="bg-yellow-300" action={createNewTemplate}>Neues Rezept</ActionButton>
</div>
