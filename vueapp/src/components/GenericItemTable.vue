<script setup>
    import { ref, onMounted } from 'vue';

    const props = defineProps(['apiPath', 'readOnly', 'itemToSave'])
    const items = ref({});

    function fetchItems() {
        items.value = {};

        fetch(props.apiPath)
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant fetch items at ${props.apiPath}`);
                }
                return r.json()
            })
            .then(json => {
                var itemsCopy = {};
                json.forEach((item, _) => itemsCopy[item.id] = item)
                items.value = itemsCopy;
                return;
            });
    }

    function removeItem(id) {
        fetch(`${props.apiPath}/${id}`, { method: "DELETE" })
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant remove item at ${props.apiPath} (id: ${id})`);
                }
                return r.json()
            })
            .then(json => {
                var itemsCopy = { ...items.value };
                delete itemsCopy[id];
                items.value = itemsCopy;
                console.log(id, "deleted?", json.isDeleted); 
                return;
            });
    }

    function saveItem() {
        fetch(props.apiPath, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(props.itemToSave)
        })
            .then(r => {
                if (!r.ok) {
                    throw new Error(`cant save item at ${props.apiPath}`);
                }
                return r.json()
            })
            .then(json => {
                var itemsCopy = { ...items.value };
                var newItem = { ...props.itemToSave };
                newItem.id = json.idOfCreatedObject;
                itemsCopy[json.idOfCreatedObject] = newItem;
                items.value = itemsCopy;
                console.log("saved", json.idOfCreatedObject);
                return;
            });
    }

    onMounted(fetchItems);
</script>

<template>
    <div class="post">
        <div class="content">
            <table>
                <thead>
                    <tr>
                        <slot name="header-row"></slot>
                        <th v-if="!readOnly">-</th>
                    </tr>
                    <tr v-if="!readOnly">
                        <slot name="input-row"></slot>
                        <td>
                            <button @click="saveItem">➕</button>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, _) in items">
                        <slot name="row" v-bind="item"></slot>
                        <td v-if="!readOnly"> 
                            <button @click="removeItem(item.id)">❌</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>