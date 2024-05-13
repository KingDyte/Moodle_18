<script setup>
import { until, invoke } from '@vueuse/core'
import {Head} from '@inertiajs/vue3';
import {computed, onBeforeMount, onMounted} from 'vue';
import store from '@/Store/index.js'
import AuthenticatedLayout from '@/Layouts/AuthenticatedLayout.vue';
import PrimaryButton from '@/Components/PrimaryButton.vue';

import Notification from "@/Components/Notification.vue";
import Courses from "@/Components/Courses.vue";
import Users from "@/Components/Users.vue";

const props = defineProps({
    loggedUser: {
        type: Object,
    }
});

const showNotification = computed(() => {
    return store.state.general.showNotification;
});

onBeforeMount(() => {
    store.commit('general/updateLoggedUser', props.loggedUser);
    store.dispatch('courses/getCourses');
});

onMounted( () => {
    const courses = store.getters['general/getLoggedUserCourses'];
    for(let i=0; i<courses.length; i++) {
        window.Echo.private('course.'+courses[i])
            .listen('Course', (e) => {
                //todo utolsót jeleníti meg, async stb
                store.dispatch('general/showSuccessNotification', e);
                console.log(e);
            });
    }
});

const getCourses = () => {
    store.commit('users/toggleShowList', false);
    store.commit('courses/setIsMy', false);
    store.dispatch('courses/getCourses');
};

const getMyCourses = () => {
    store.commit('users/toggleShowList', false);
    store.commit('courses/setIsMy', true);
    store.dispatch('courses/getCourses');
};

const uploadFile = () => {
    if (!store.getters['general/hasPermission']('upload')) {
        store.dispatch('general/showWarningNotification');
        return;
    }
    const selectedFolder = store.getters['folders/getSelectedFolder'];
    store.commit('files/updateFileField', {
        field: 'parentFolderId',
        value: selectedFolder === null ? null : selectedFolder.id
    })
    store.commit('files/toggleFileModal', true);
};

</script>

<template>
    <Notification v-show="showNotification"/>
    <Head title="Dashboard" />

    <AuthenticatedLayout>
        <template #header>
            <PrimaryButton @click="getCourses" class="mr-2">Kurzusok</PrimaryButton>
            <PrimaryButton @click="getMyCourses" class="mr-2">Kurzusaim</PrimaryButton>
        </template>

        <Courses />
        <Users />
    </AuthenticatedLayout>
</template>
