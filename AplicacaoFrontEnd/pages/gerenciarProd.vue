<template>
    <div>
        <div class="title">
            <h3 style="color: black">Gerenciar Produtos</h3>
        </div>
        <div class="option">
            <router-link to="/registrarProd" class="button">Registrar</router-link>
        </div>
        <div class="option">
            <router-link to="/consultarProd" class="button">Consultar</router-link>
        </div>
    </div>
</template>

<style scoped>
.title {
    margin-bottom: 10px;
    padding: 10px;
    background-color: white;
    border-radius: 5px;
    text-align: center;
    font-weight: bold;
}

.option {
    margin-bottom: 10px;
    text-align: center;
}

.button {
    display: inline-block;
    padding: 10px;
    background-color: #AC0B0B;
    border-radius: 5px;
    text-decoration: none;
    color: black;
    font-size: large;
    font-weight: bold;
    width: 300px;
}

.button:hover {
    background-color: white;
    color: black;
}
</style>