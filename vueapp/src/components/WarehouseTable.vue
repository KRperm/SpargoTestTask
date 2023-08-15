<script setup>
    import GenericItemTable from './GenericItemTable.vue';
    import { ref, onMounted } from 'vue';

    const emit = defineEmits(['switchTab']);
    const pharmacies = ref({});
    const itemToSave = ref({});

    function fetchPharmacies() {
        pharmacies.value = {};

        fetch('api/pharmacy')
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant fetch api/pharmacy at ${props.apiPath}`);
                }
                return r.json()
            })
            .then(json => {
                json.forEach((pharmacy, _) => pharmacies.value[pharmacy.id] = pharmacy)
                return;
            });
    }

    onMounted(fetchPharmacies)
</script>

<template>
    <GenericItemTable api-path="api/warehouse" :item-to-save="itemToSave">
        <template #header-row>
            <th>Аптека</th>
            <th>Имя склада</th>
            <th>Партии склада</th>
        </template>
        <template #input-row>
            <td>
                <select v-model="itemToSave.pharmacyId">
                    <option v-for="pharmacy in pharmacies" :value="pharmacy.id">{{ pharmacy.name }}</option>
                </select>
            </td>
            <td>
                <input v-model="itemToSave.name" />
            </td>
            <td>-</td>
        </template>
        <template #row="{ id, pharmacyId, name }">
            <td>{{ pharmacies[pharmacyId]?.name ?? "?" }}</td>
            <td>{{ name }}</td>
            <td>
                <button @click="emit('switchTab', 'ShipmentInWarehouseTable', id)">➔</button>
            </td>
        </template>
    </GenericItemTable>
</template>