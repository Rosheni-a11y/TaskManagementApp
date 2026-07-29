<template>
    <div class="min-h-screen w-full app-gradient-bg">
        <Navbar />
        <div class="flex items-center justify-center px-6" style="min-height: calc(100vh - 68px);">
            <AddUserForm />
        </div>
    </div>
</template>

<script lang="ts">
import Navbar from '../components/Navbar.vue';
import AddUserForm from '../components/AddUserForm.vue';

export default {
    name: 'SignupView',
    components: { Navbar, AddUserForm }
};
</script>