<script setup>
    import { ref } from 'vue';
    import GenericItemTable from './GenericItemTable.vue';
    const itemToSave = ref({});
</script>

<template>
    <GenericItemTable api-path="api/product" :item-to-save="itemToSave">
        <template #header-row>
            <th>Имя товара</th>
        </template>
        <template #input-row>
            <td>
                <input v-model="itemToSave.name" />
            </td>
        </template>
        <template #row="{ name }">
            <td>{{ name }}</td>
        </template>
    </GenericItemTable>
</template>