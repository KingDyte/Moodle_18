<script setup>
import {computed, watch} from "vue";
import store from "@/Store/index.js";
import CheckIcon from "@/Components/Svg/CheckIcon.vue";
import ErrorIcon from "@/Components/Svg/ErrorIcon.vue";
import WarningIcon from "@/Components/Svg/WarningIcon.vue";
import XIcon from "@/Components/Svg/XIcon.vue";

const notificationData = computed(() => {
    return store.state.general.notificationData;
})

const showNotification = computed(() => {
    return store.state.general.showNotification;
});
const closeNotification = () => {
    store.commit('general/toggleShowNotification', false);
}

watch(showNotification, (newValue) => {
    if (newValue === true) {
        setTimeout(function () {
            closeNotification();
        }, 3000);
    }
});
</script>

<template>
    <Transition name="slide-fade">
        <div
            class="flex items-center fixed right-0 bottom-0 z-10 max-w-xs p-4 mb-4 mr-4 text-gray-500 bg-white rounded-lg shadow"
            role="alert">
            <CheckIcon v-if="notificationData.type === 'success'"/>
            <ErrorIcon v-if="notificationData.type === 'error'"/>
            <WarningIcon v-if="notificationData.type === 'warning'"/>
            <div class="ml-2 text-sm font-normal">
                {{ notificationData.title }}<br />
                {{ notificationData.content }}
            </div>
            <XIcon  @click="closeNotification" />
        </div>
    </Transition>
</template>

<style scoped>
.slide-fade-enter-active {
    transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
    transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
    transform: translateX(250px);
    opacity: 0;
}
</style>
