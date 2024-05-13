<script setup>
import {computed} from "vue";
import store from "@/Store/index.js";
import Dropdown from '@/Components/Dropdown.vue';
import DropdownLink from '@/Components/DropdownLink.vue';

const showList = computed(() => {
    return store.getters['courses/isVisible'];
});

const selectDegree = (degree, degreeName) => {
    store.commit('courses/selectDegree', {id: degree, name: degreeName});
    store.dispatch('courses/getCourses');
};

const users = (courseId) => {
    store.commit('users/selectCourse', courseId);
    store.dispatch('users/getUsers');
    store.commit('courses/toggleShowList', false)
};

const apply = (courseId) => {
    store.commit('courses/selectCourse', courseId);
    store.dispatch('courses/apply');
};

const allowApply = (course) => {
    return !(store.getters['courses/getExistsCourses'].includes(course.id))
    && course.degrees.map((item) => {return item.id}).includes(store.getters['general/getLoggedUserDegree'])
}

</script>

<template>
    <div v-show="showList">
        <div class="mx-auto sm:px-6 lg:px-8">
            <div class="bg-white overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-4 text-gray-900">
                    <div class="float-left">
                        <span v-if="store.getters['courses/getIsMy']">Kurzusaim</span>
                        <span v-else>Kurzusok</span>
                    </div>
                    <div class="float-right">
                        <Dropdown align="right" width="48">
                            <template #trigger>
                                <span class="inline-flex rounded-md">
                                    <button
                                        type="button"
                                        class="inline-flex items-center px-3 py-2 border border-transparent text-sm leading-4 font-medium rounded-md text-gray-500 bg-white hover:text-gray-700 focus:outline-none transition ease-in-out duration-150"
                                    >
                                        <span v-if="store.getters['courses/getSelectedDegreeName'] !== null">{{store.getters['courses/getSelectedDegreeName']}}</span>
                                        <span v-else>Szakok</span>

                                        <svg
                                            class="ml-2 -mr-0.5 h-4 w-4"
                                            xmlns="http://www.w3.org/2000/svg"
                                            viewBox="0 0 20 20"
                                            fill="currentColor"
                                        >
                                            <path
                                                fill-rule="evenodd"
                                                d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                                clip-rule="evenodd"
                                            />
                                        </svg>
                                    </button>
                                </span>
                            </template>

                            <template #content>
                                <DropdownLink href="#" @click="selectDegree(null, null)" method="get" as="button">
                                    Összes
                                </DropdownLink>
                                <DropdownLink
                                    v-for="(degree, degreeId) in store.getters['courses/getDegrees']"
                                    @click="selectDegree(degreeId, degree)" method="get" as="button"
                                    href="#"
                                >
                                    {{ degree}}
                                </DropdownLink>
                            </template>
                        </Dropdown>
                    </div>
                    &nbsp;
                </div>
                <div class="relative overflow-x-auto shadow-md px-4 pb-4">
                    <table class="w-full text-sm text-left rtl:text-right text-gray-900">
                        <thead class="text-xs text-gray-700 uppercase bg-gray-100">
                        <tr>
                            <th scope="col" class="px-6 py-3">
                                Kód
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Megnevezés
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Kredit
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Szakok
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Action
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr class="border-b"
                            v-if=" store.getters['courses/getCourses'].length > 0"
                            v-for="course in store.getters['courses/getCourses']">
                            <th scope="row" class="px-6 py-4">
                                {{course.code}}
                            </th>
                            <td class="px-6 py-4">
                                {{course.name}}
                            </td>
                            <td class="px-6 py-4">
                                {{course.credit}}
                            </td>
                            <td class="space-x-4" >
                                <span v-for="degree in course.degrees">
                                    {{degree.name}}
                                </span>
                            </td>
                            <td class="px-6 py-4">
                                <a href="#"
                                   class="font-medium text-blue-600 dark:text-blue-500 float-left"
                                   @click="users(course.id)" title="Jelentkezők">
                                    <svg class="w-6 h-6 text-gray-800 hover:text-gray-600" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M8 5.00005C7.01165 5.00082 6.49359 5.01338 6.09202 5.21799C5.71569 5.40973 5.40973 5.71569 5.21799 6.09202C5 6.51984 5 7.07989 5 8.2V17.8C5 18.9201 5 19.4802 5.21799 19.908C5.40973 20.2843 5.71569 20.5903 6.09202 20.782C6.51984 21 7.07989 21 8.2 21H15.8C16.9201 21 17.4802 21 17.908 20.782C18.2843 20.5903 18.5903 20.2843 18.782 19.908C19 19.4802 19 18.9201 19 17.8V8.2C19 7.07989 19 6.51984 18.782 6.09202C18.5903 5.71569 18.2843 5.40973 17.908 5.21799C17.5064 5.01338 16.9884 5.00082 16 5.00005M8 5.00005V7H16V5.00005M8 5.00005V4.70711C8 4.25435 8.17986 3.82014 8.5 3.5C8.82014 3.17986 9.25435 3 9.70711 3H14.2929C14.7456 3 15.1799 3.17986 15.5 3.5C15.8201 3.82014 16 4.25435 16 4.70711V5.00005M15 12H12M15 16H12M9 12H9.01M9 16H9.01" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                                    </svg>                                </a>
                                <a href="#"
                                   v-if="allowApply(course)"
                                   class="font-medium text-blue-600 dark:text-blue-500 float-left"
                                   @click="apply(course.id)" title="Jelentkezés">
                                    <svg class="w-6 h-6 text-gray-800 hover:text-gray-600" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" >
                                        <path d="M4 12.6111L8.92308 17.5L20 6.5" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                                    </svg>
                                </a>
                            </td>
                        </tr>
                        <tr v-else class="border-b">
                            <td class="px-6 py-4 text-center" colspan="7">Nincs megjeleníthető kurzus</td>
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
