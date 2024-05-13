<script setup>
import {computed} from "vue";
import store from "@/Store/index.js";

const showUserList = computed(() => {
    return store.getters['users/isVisible'];
});

const backToCourses = () => {
    store.commit('users/toggleShowList', false);
    store.commit('courses/toggleShowList', true);
};

</script>

<template>
    <div v-show="showUserList">
        <div class="mx-auto sm:px-6 lg:px-8">
            <div class="bg-white overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-4 text-gray-900">
                    <div class="float-left">
                        <span>{{store.getters['users/getCourse'].name }} kurzushoz tartozó jelentkezők névsora</span>
                    </div>
                    <div class="float-right">
                        <a href="#"
                           class="font-medium text-blue-600 dark:text-blue-500 float-left"
                           @click="backToCourses()" title="Vissza">
                            <svg class="w-6 h-6 text-gray-800 hover:text-gray-600" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M3 8H16.5C18.9853 8 21 10.0147 21 12.5C21 14.9853 18.9853 17 16.5 17H3M3 8L6 5M3 8L6 11" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                            </svg>
                        </a>
                    </div>
                    &nbsp;
                </div>
                <div class="relative overflow-x-auto shadow-md px-4 pb-4">
                    <table class="w-full text-sm text-left rtl:text-right text-gray-900">
                        <thead class="text-xs text-gray-700 uppercase bg-gray-100">
                        <tr>
                            <th scope="col" class="px-6 py-3">
                                Név
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Email
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr class="border-b"
                            v-if=" store.getters['users/getUsers'].length > 0"
                            v-for="user in store.getters['users/getUsers']">
                            <th scope="row" class="px-6 py-4">
                                {{user.name}}
                            </th>
                            <td class="px-6 py-4">
                                {{user.email}}
                            </td>
                        </tr>
                        <tr v-else class="border-b">
                            <td class="px-6 py-4 text-center" colspan="7">A kurzusra még nem jelentkezett senki</td>
                        </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.v-enter-active,
.v-leave-active {
    transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
    opacity: 0;
}
</style>
