<script setup>
    import GenericItemTable from './GenericItemTable.vue';
    import { ref, onMounted } from 'vue';

    const products = ref({});
    const warehouses = ref({});
    const itemToSave = ref({});

    function fetchItems() {
        products.value = {};
        warehouses.value = {};

        fetch('api/product')
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant fetch api/product at ${props.apiPath}`);
                }
                return r.json()
            })
            .then(json => {
                json.forEach((product, _) => products.value[product.id] = product)
                return;
            });

        fetch('api/warehouse')
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant fetch api/warehouse at ${props.apiPath}`);
                }
                return r.json()
            })
            .then(json => {
                json.forEach((warehouse, _) => warehouses.value[warehouse.id] = warehouse)
                return;
            });
    }

    onMounted(fetchItems)
</script>

<template>
    <GenericItemTable api-path="api/shipment" :item-to-save="itemToSave">
        <template #header-row>
            <th>Товар</th>
            <th>Содержится в складе</th>
            <th>Количество</th>
        </template>
        <template #input-row>
            <td>
                <select v-model="itemToSave.productId">
                    <option v-for="product in products" :value="product.id">{{ product.name }}</option>
                </select>
            </td>
            <td>
                <select v-model="itemToSave.warehouseId">
                    <option v-for="warehouse in warehouses" :value="warehouse.id">{{ warehouse.name }}</option>
                </select>
            </td>
            <td>
                <input v-model.number="itemToSave.amount" />
            </td>
        </template>
        <template #row="{ productId, warehouseId, amount }">
            <td>{{ products[productId]?.name ?? "?" }}</td>
            <td>{{ warehouses[warehouseId]?.name ?? "?" }}</td>
            <td>{{ amount }}</td>
        </template>
    </GenericItemTable>
</template>