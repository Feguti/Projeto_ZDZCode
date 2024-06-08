<template>
    <div>
        <v-text-field v-model="dados.id" label="ID" readonly></v-text-field>
        <v-text-field v-model="dados.nome" label="Nome"></v-text-field>
        <v-text-field v-model="dados.tipo" label="Tipo"></v-text-field>
        <v-text-field v-model="dados.categoria" label="Categoria"></v-text-field>
        <v-text-field v-model="dados.descricao" label="Descrição"></v-text-field>

        <div class="d-flex flex-column">
            <v-btn class="mt-4" color="success" block @click="updateProduto">
                Atualizar
            </v-btn>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            dados: {
                id: '',
                nome: '',
                tipo: '',
                categoria: '',
                descricao: '',
            },
        };
    },
    created() {
        this.dados.id = this.$route.params.id;
        this.dados.nome = this.$route.params.nome;
        this.dados.tipo = this.$route.params.tipo;
        this.dados.categoria = this.$route.params.categoria;
        this.dados.descricao = this.$route.params.descricao;
    },
    methods: {
        async updateProduto() {
            try {
                const response = await fetch(`https://localhost:44353/Produtos/${this.dados.id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(this.dados),
                });

                if (!response.ok) {
                    throw new Error('Erro ao atualizar produto');
                }

                const produtoAtualizado = await response.json();
                console.log('Produto atualizado:', produtoAtualizado);

                alert('Produto atualizado com sucesso!');
            } catch (error) {
                console.error('Erro:', error.message);
                alert('Erro ao atualizar produto. Por favor, tente novamente.');
            }
        },
    },
};
</script>