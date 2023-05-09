<script lang="ts">
    import {onMount} from "svelte";
    import apiClient from "../../communication/api/api-client";
    import type { Template } from "./Template";
    import TemplateData from "./TemplateData.svelte";
    import { navigate } from "svelte-routing";

    let template : Template;
    export let id: string;

    onMount(async () => {
       
        if (!!id){
            template = await apiClient.getTemplateAsync(id);
        }  
    });

    async function handleSubmit(event){

        console.log(event.detail)
        const modifiedTemplate = event.detail as Template;
        await apiClient.updateTemplateAsync(modifiedTemplate);
        navigate('/templates');
    }


</script>
{#if template != undefined}
<TemplateData template={template} on:submit={handleSubmit} />
{/if}