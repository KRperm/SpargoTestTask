<script setup>
    import GenericItemTable from './GenericItemTable.vue';
    import { ref, onMounted } from 'vue';

    const props = defineProps(['tabData']);
    const products = ref({});

    function fetchItems() {
        products.value = {};

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
    }

    onMounted(fetchItems)
</script>

<template>
    <GenericItemTable :api-path="`api/shipment/inwarehouse/${tabData}`" :read-only="true">
        <template #header-row>
            <th>Товар</th>
            <th>Количество</th>
        </template>
        <template #row="{ productId, amount }">
            <td>{{ products[productId]?.name ?? "?" }}</td>
            <td>{{ amount }}</td>
        </template>
    </GenericItemTable>
</template>