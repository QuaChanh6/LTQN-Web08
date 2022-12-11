<template>
    <div class="list-data" v-clickoutside="hideDropList">
        <div class="data" v-for="(data, index) in dataDropList" :key="index" @click="sendMessage(data.type)" >{{data.text}}</div>
    </div>
 
</template>
  
<script>


const clickoutside = {
  mounted(el, binding) {
    el.clickOutsideEvent = (event) => {
      //click ra ngoài
      // event: đối tượng click
      if (
        !(( el === event.target || // click phạm vi của combobox-value
            el.previousElementSibling.contains(event.target)
          )) //click vào ele trước combobox-value
        ) 
      {
        binding.value();
      }
    };
    document.body.addEventListener("click", el.clickOutsideEvent);
  },
  beforeUnmount: (el) => {
    document.body.removeEventListener("click", el.clickOutsideEvent);
  },
};

export default {

    props: {
        emp: Object,
        dataDropList: Array,
        index: Number
    },
    methods: {        
        sendMessage(tool) { //tool: xóa, nhân bản, ngừng sử dụng
            this.$emit('tool', {tool: tool, emp: this.emp, index: this.index});
        },

        hideDropList(){
            this.$emit('closeDropList');
        }
    },
    directives: {
      clickoutside,
    },

    
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  