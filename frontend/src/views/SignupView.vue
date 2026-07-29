<template>
    <div class="min-h-screen w-full flex items-center justify-center px-6 app-gradient-bg">
        <AddUserForm />
    </div>
</template>

<script lang="ts">
import AddUserForm from '../components/AddUserForm.vue';

export default {
    name: 'SignupView',
    components: { AddUserForm }
};
</script>