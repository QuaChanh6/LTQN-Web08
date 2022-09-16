<template>
    <div class="list-data" >
        <div class="data" v-for="(data, index) in dataDropList" :key="index" @click="sendMessage(data.type)" >{{data.text}}</div>
    </div>
 
</template>
  
<script>


export default {

    props: {
        emp: Object,
        dataDropList: Array
    },

    methods: {        
        sendMessage(tool) { //tool: xóa, nhân bản, ngừng sử dụng
            this.$emit('tool', {tool: tool, emp: this.emp});
        },
    },
    data() {
        return {
        };
    },
    components: {}
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  