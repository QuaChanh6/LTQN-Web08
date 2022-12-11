<template>
    <div class="container-toast" :class="type">
        <div class="toast-message-container">
            <div class="toast-message-icon"><div class="icon"></div></div>
            <div class="toast-message-content">{{content}}</div>
            <div class="close-icon" @click="closeToast"><div class="icon-close-toast-message"></div></div>
        </div>
    </div>
</template>
  
<script>
  export default {
    created(){
     
    },
    props: {
        show: Boolean,
        content: String,
        type: String
    },
    methods: {
        closeToast(){
            this.$emit('closeToastMessage');
        },
    },
    data(){
        return{
        }
    },
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
 @import url(../../css/base/toastMessage.css);
</style>
  