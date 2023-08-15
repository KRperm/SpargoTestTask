<script setup>
    import { ref } from 'vue';
    import GenericItemTable from './GenericItemTable.vue';
    const emit = defineEmits(['switchTab']);
    const itemToSave = ref({});
</script>

<template>
    <GenericItemTable api-path="api/pharmacy" :item-to-save="itemToSave">
        <template #header-row>
            <th>Название аптеки</th>
            <th>Адрес</th>
            <th>Телефон</th>
            <th>Товары аптеки</th>
        </template>
        <template #input-row>
            <td>
                <input v-model="itemToSave.name" />
            </td>
            <td>
                <input v-model="itemToSave.address" />
            </td>
            <td>
                <input v-model="itemToSave.phone" />
            </td>
            <td>-</td>
        </template>
        <template #row="{ id, name, address, phone }">
            <td>{{ name }}</td>
            <td>{{ address }}</td>
            <td>{{ phone }}</td>
            <td>
                <button @click="emit('switchTab', 'ProductInPharmacyTable', id)">➔</button>
            </td>
        </template>
    </GenericItemTable>
</template>