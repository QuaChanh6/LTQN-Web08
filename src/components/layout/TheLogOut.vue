<template>
<div class="log-out" v-clickoutside="hideLogOut">
    <div class="content-log-out" @click="logOut()">
        <vue-fontawesome :icon="'sign-out'" size="1"></vue-fontawesome>
        <div>Đăng xuất</div>
    </div>
    <div  class="content-log-out" style="margin-top: 8px;" @click="password()">
      <div class="icon-pen-small"></div>
      <div>Đổi mật khẩu</div>
    </div>
</div>
</template>

<script>
/**
 * Click ra ngoài thì ẩn
 * author: LTQN(8/10/2022)
 */
 const clickoutside = {
  mounted(el, binding) {
    el.clickOutsideEvent = (event) => {
      //click ra ngoài
      // event: đối tượng click
      if (
        !(( // click phạm vi của combobox-value || click vào ele trước combobox-value
            el === event.target || el.previousElementSibling.contains(event.target)
          ))
      ) {
        // thực hiện giá trị truyền vào
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
    directives: {
      clickoutside,
    },
    methods:{
        logOut(){
            this.$emit('logOut');
        },
        hideLogOut(){
            this.$emit('hideLogOut');
        },
        password(){
          this.$emit('pass');
        }
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/logOut.css);
</style>
  