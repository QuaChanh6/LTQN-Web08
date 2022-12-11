<template>
    <div class="pop-up-container warning">
            <div class="container"></div>
            <div class="pop-up">
                <div class="pop-up-content">
                    <div class="icon">
                        <div :class='type'></div>
                    </div>
                    <div class="pop-up-text">{{text}}</div>
                </div>
                <div class="pop-up-action">
                    <button class="button" @click="closeWarning" ref="focus" tabindex="-1">{{textClose}}</button>
                </div>
            </div>
                
    </div>
</template>
  
<script>
    export default {
        props: {
            text: String,
            type: String
        },
        mounted(){
            this.$refs.focus.focus();
        },
        data(){
            return{
                textClose: 'Đóng'
            }
        },
        methods: {
            closeWarning(){
                this.$emit('closeWarning');
            }
        }
    }
</script>

  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/popup.css);
</style>
  