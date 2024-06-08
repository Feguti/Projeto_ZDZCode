<template>
    <v-sheet class="mx-auto">
        <div class="title-container">
            <h2 class="title-text">Registrar novo produto</h2>
        </div>

        <v-form ref="form" class="form-container">
            <v-text-field v-model="dados.nome" :rules="generalRules" label="Nome" required></v-text-field>

            <v-text-field v-model="dados.tipo" label="Tipo"></v-text-field>

            <v-select v-model="dados.categoria" :items="items" :rules="generalRules" label="Categoria"
                required></v-select>

            <v-text-field v-model="dados.descricao" :rules="generalRules" label="Descrição" required></v-text-field>

            <div class="button">
                <v-btn color="#11A634" block @click="submit">
                    Registrar
                </v-btn>
            </div>

        </v-form>
    </v-sheet>
</template>

<style scoped>
.title-container {
    background-color: white;
    padding: 10px;
    text-align: center;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
}

.title-text {
    color: black;
    margin: 0;
}

.button {
    margin-top: 20px;
}

.mx-auto {
    background-color: #272728;
    border-radius: 50px;
}
</style>


<script>
export default {
    data: () => ({

        dados: {
            nome: '',
            tipo: '',
            categoria: '',
            descricao: '',
        },

        generalRules: [
            v => !!v || 'O campo é obrigatório',
            v => (v && v.length >= 3) || 'Precisa ter pelo menos 3 caracteres',
        ],
        select: null,
        items: [
            'Medicamento',
            'Cosmético',
            'Utensílio',
        ],
    }),

    methods: {
        async submit() {

            if (this.dados.nome.length < 3 || this.dados.categoria.length < 3 || this.dados.descricao.length < 3) {
                alert('Por favor, preencha os campos Nome, Categoria e Descrição com pelo menos 3 caracteres.');
                return;
            }
            try {

                var response = await this.$axios({
                    method: 'POST',
                    url: 'https://localhost:44353/Produtos/registrar',
                    data: this.dados,
                });
            }
            catch {

            }
        }
    },
}
</script>