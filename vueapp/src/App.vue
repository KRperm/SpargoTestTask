<script setup>
    import PharmacyTable from './components/PharmacyTable.vue';
    import ProductTable from './components/ProductTable.vue';
    import ShipmentTable from './components/ShipmentTable.vue';
    import WarehouseTable from './components/WarehouseTable.vue';
    import ProductInPharmacyTable from './components/ProductInPharmacyTable.vue';
    import ShipmentInWarehouseTable from './components/ShipmentInWarehouseTable.vue';
    import { ref } from 'vue';

    const tabData = ref('')
    const currentTabName = ref('PharmacyTable')
    const allTabs = {
        PharmacyTable,
        ProductTable,
        ShipmentTable,
        WarehouseTable,
        ProductInPharmacyTable,
        ShipmentInWarehouseTable,
    }
    const selectableTabs = {
        'PharmacyTable': 'Аптеки',
        'ProductTable': 'Товарные наименования',
        'ShipmentTable': 'Партии',
        'WarehouseTable': 'Склады'
    };

    function onSwitchingTab(tabName, newTabData) {
        tabData.value = newTabData;
        currentTabName.value = tabName;
        console.log("switch", tabName, tabData.value);
    }
</script>

<template>
    <header>
        <div class="wrapper">
            <button v-for="(tabDescription, tabName) in selectableTabs"
                    :class="['tab-button', { active: currentTabName === tabName }]"
                    @click="currentTabName = tabName">
                {{ tabDescription }}
            </button>
        </div>
    </header>

    <main>
        <component :is="allTabs[currentTabName]" @switch-tab="onSwitchingTab" :tab-data="tabData"></component>
    </main>
</template>

<style scoped>
    header {
        line-height: 1.5;
    }

    .tab-button
    {
        border: none;
        padding: 10px;
        cursor: pointer;
    }

    .tab-button.active {
        background: lightblue;
    }
</style>
