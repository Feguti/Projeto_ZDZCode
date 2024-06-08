<template>
    <v-sheet class="mx-auto">
        <div class="title-container">
            <h2 class="title-text">Registrar novo funcionário</h2>
        </div>

        <v-form ref="form" class="form-container">
            <v-text-field v-model="dados.nome" :rules="nameRules" label="Nome" required></v-text-field>

            <v-text-field v-model="dados.idade" :rules="ageRules" label="Idade" required></v-text-field>

            <v-text-field v-model="dados.funcao" :rules="functionRules" label="Função" required></v-text-field>

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
            idade: '',
            funcao: '',
        },

        nameRules: [
            v => !!v || 'Nome é obrigatório',
            v => (v && v.length >= 3) || 'Nome precisa ter pelo menos 3 caracteres',
        ],

        ageRules: [
            v => !!v || 'Idade é obrigatória',
        ],

        functionRules: [
            v => !!v || 'Função é obrigatória',
            v => (v && v.length >= 3) || 'Função precisa ter pelo menos 3 caracteres',
        ],
    }),

    methods: {
        async submit() {
            try {
                if (!this.dados.nome || !this.dados.funcao || !this.dados.idade) {
                    alert('Todos os campos devem ser preenchidos');
                    return;
                }

                var response = await this.$axios({
                    method: 'POST',
                    url: 'https://localhost:44353/Funcionario',
                    data: this.dados,
                });
            }
            catch {

            }
        }
    },
}
</script>