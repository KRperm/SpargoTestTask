<script setup>
    import { ref } from 'vue';
    import GenericItemTable from './GenericItemTable.vue';
    const props = defineProps(['tabData']);
</script>

<template>
    <GenericItemTable :api-path="`api/product/inpharmacy/${tabData}`" :read-only="true">
        <template #header-row>
            <th>Имя товара</th>
            <th>Общее количество</th>
        </template>
        <template #row="{ name, totalAmount }">
            <td>{{ name }}</td>
            <td>{{ totalAmount }}</td>
        </template>
    </GenericItemTable>
</template>