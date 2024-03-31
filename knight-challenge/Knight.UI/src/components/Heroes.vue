<script lang="ts">
import { getHeroes } from "@/http/knight";
import type IKnight from "@/interfaces/IKnight";

export default {
    name: 'Knights',
    data() {
        return {
            tableHeaders: ['Name', 'Age', 'Weapons', 'Attribute', 'Attack', 'Exp'],
            knights: [] as IKnight[],
        }
    },
    async created() {
        this.knights = await getHeroes();
    }
}
</script>

<template>
    <div class="container box">
        <div class="table-container">
            <table class="table block">
                <thead class="is-full">
                    <tr>
                        <th v-for="head in tableHeaders">{{ head }}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="knight in knights">
                        <td>{{ knight.name }}</td>
                        <td>{{ knight.age }}</td>
                        <td>{{ knight.weapons }}</td>
                        <td>{{ knight.attribute }}</td>
                        <td>{{ knight.attack }}</td>
                        <td>{{ knight.experience }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<style scoped>
.block{
    margin-left: auto;
    margin-right: auto;
    width: 100%;
}
</style>