<template>
  
  <TheSidebar/>
  <TheContentWrapper/>
  <TheContainer/>
</template>

<script>
import TheSidebar from './components/layout/TheSidebar.vue';
import TheContentWrapper from './components/view/employee/TheContentWrapper.vue';
import TheContainer from './components/view/employee/TheContainer.vue';
export default {
  name: 'App',
  components: {
    TheSidebar,
    TheContentWrapper,
    TheContainer
}
}
</script>

<style>
  @import url(../src/css/main.css);
  @import url(../src/css/base/input.css);
</style>
