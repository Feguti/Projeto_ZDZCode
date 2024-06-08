<template>
    <v-sheet class="mx-auto">
        <div class="title-container">
            <h2 class="title-text">Consultar produtos em estoque</h2>
        </div>
        <v-form ref="form" class="form-container">

            <v-text-field v-model="dados.nome" label="Nome"></v-text-field>

            <v-text-field v-model="dados.descricao" label="Descrição"></v-text-field>

            <div class="button">
                <v-btn color="#11A634" block @click="buscar">
                    Buscar
                </v-btn>
            </div>
            <v-list v-if="resultados.length">
                <v-list-item v-for="(produtos, index) in resultados" :key="index">
                    <v-list-item-content>
                        <v-list-item-title>{{ produtos.nome }}</v-list-item-title>
                        <v-list-item-subtitle>ID: {{ produtos.id }}</v-list-item-subtitle>
                        <v-list-item-subtitle>Tipo: {{ produtos.tipo }}</v-list-item-subtitle>
                        <v-list-item-subtitle>Categoria: {{ produtos.categoria }}</v-list-item-subtitle>
                        <v-list-item-subtitle>Descrição: {{ produtos.descricao }}</v-list-item-subtitle>
                    </v-list-item-content>
                    <v-list-item-action>
                        <v-btn color="primary" @click="editarProduto(produtos)">
                            Editar
                        </v-btn>
                        <v-btn color="error" @click="excluirProduto(produtos)">
                            Excluir
                        </v-btn>
                    </v-list-item-action>
                </v-list-item>
            </v-list>
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
            id: null,
            nome: '',
            id: '',
            tipo: '',
            categoria: '',
            descricao: '',
        },
        resultados: [],
    }),
    methods: {
        async buscar() {
            try {
                if (!this.dados.nome && !this.dados.descricao) {
                    alert('Pelo menos um dos campos deve ser preenchido');
                    return;
                }

                const params = new URLSearchParams();
                if (this.dados.nome) params.append('nome', this.dados.nome);
                if (this.dados.descricao) params.append('descricao', this.dados.descricao);

                const response = await this.$axios({
                    method: 'GET',
                    url: `https://localhost:44353/Produtos/consultar?${params.toString()}`,
                });

                this.resultados = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados:", error);
            }
        },
        editarProduto(produtos) {
            this.$router.push({
                name: 'atualizarProd',
                params: {
                    id: produtos.id,
                    nome: produtos.nome,
                    categoria: produtos.categoria,
                    tipo: produtos.tipo,
                    descricao: produtos.descricao,
                },
            });
        },
        async excluirProduto(produtos) {
            if (confirm(`Tem certeza que deseja excluir o produto ${produtos.nome}?`)) {
                try {
                    await this.$axios({
                        method: 'DELETE',
                        url: `https://localhost:44353/Produtos/excluir/${produtos.id}`,
                    });
                    this.resultados = this.resultados.filter(f => f.id !== produtos.id);
                } catch (error) {
                    console.error("Erro ao excluir produto:", error);
                }
            }
        }
    }
}
</script>