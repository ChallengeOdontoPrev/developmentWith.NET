// wwwroot/js/author-dashboard.js

document.addEventListener('DOMContentLoaded', function () {
    // Inicializa tooltips do Bootstrap
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });

    // Inicializa os modais
    var modals = {
        edit: new bootstrap.Modal(document.getElementById('editPostModal')),
        delete: new bootstrap.Modal(document.getElementById('deleteConfirmModal')),
        create: new bootstrap.Modal(document.getElementById('newPostModal'))
    };

    // Função para editar post (adicione ao author-dashboard.js)
    window.editPost = function (postId) {
        // Mostra loading no botão
        const editButton = event.target.closest('button');
        const originalContent = editButton.innerHTML;
        editButton.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Carregando...';
        editButton.disabled = true;

        fetch(`/api/Post/${postId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erro ao carregar o post');
                }
                return response.json();
            })
            .then(post => {
                document.getElementById('editPostId').value = post.id;
                document.getElementById('editTitle').value = post.title;
                document.getElementById('editContent').value = post.content;
                document.getElementById('editCreatedAt').value = post.createdAt;
                const editModal = new bootstrap.Modal(document.getElementById('editPostModal'));
                editModal.show();
            })
            .catch(error => {
                showToast('Erro ao carregar o post. Tente novamente.', 'danger');
            })
            .finally(() => {
                // Restaura o botão
                editButton.innerHTML = originalContent;
                editButton.disabled = false;
            });
    }

    // Função para confirmar exclusão
    window.confirmDelete = function (postId) {
        document.getElementById('deletePostId').value = postId;
        modals.delete.show();
    }

    // Sistema de Toast para notificações
    window.showToast = function (message, type = 'success') {
        const toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            const container = document.createElement('div');
            container.id = 'toast-container';
            container.className = 'position-fixed bottom-0 end-0 p-3';
            container.style.zIndex = '1050';
            document.body.appendChild(container);
        }

        const toastElement = document.createElement('div');
        toastElement.className = `toast align-items-center text-white bg-${type} border-0`;
        toastElement.setAttribute('role', 'alert');
        toastElement.setAttribute('aria-live', 'assertive');
        toastElement.setAttribute('aria-atomic', 'true');

        toastElement.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        `;

        document.getElementById('toast-container').appendChild(toastElement);
        const toast = new bootstrap.Toast(toastElement);
        toast.show();

        // Remove o toast depois que for fechado
        toastElement.addEventListener('hidden.bs.toast', function () {
            toastElement.remove();
        });
    }

    // Validação de formulários
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
                showToast('Por favor, preencha todos os campos obrigatórios.', 'danger');
            }
            form.classList.add('was-validated');
        });
    });

    // Preview de conteúdo
    const contentInputs = document.querySelectorAll('textarea[name="Content"]');
    contentInputs.forEach(input => {
        input.addEventListener('input', function () {
            const preview = this.closest('.modal-body').querySelector('.content-preview');
            if (preview) {
                preview.innerHTML = marked(this.value); // Requer biblioteca marked.js para Markdown
            }
        });
    });

    // Manipulação de imagens (se implementado)
    const imageInputs = document.querySelectorAll('input[type="file"]');
    imageInputs.forEach(input => {
        input.addEventListener('change', function () {
            const file = this.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    const preview = input.closest('.form-group').querySelector('.image-preview');
                    if (preview) {
                        preview.src = e.target.result;
                    }
                };
                reader.readAsDataURL(file);
            }
        });
    });

    // Atalhos de teclado
    document.addEventListener('keydown', function (e) {
        // Ctrl/Cmd + N para novo post
        if ((e.ctrlKey || e.metaKey) && e.key === 'n') {
            e.preventDefault();
            modals.create.show();
        }

        // Esc para fechar modais
        if (e.key === 'Escape') {
            Object.values(modals).forEach(modal => modal.hide());
        }
    });

    // Auto-save de rascunhos (opcional)
    let autoSaveTimeout;
    const autoSave = (formData) => {
        localStorage.setItem('draft_' + formData.get('Id'), JSON.stringify({
            title: formData.get('Title'),
            content: formData.get('Content'),
            timestamp: new Date().toISOString()
        }));
    };

    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('input', function () {
            clearTimeout(autoSaveTimeout);
            autoSaveTimeout = setTimeout(() => {
                const formData = new FormData(this);
                autoSave(formData);
            }, 1000);
        });
    });
});

// Funções auxiliares
function formatDate(date) {
    return new Date(date).toLocaleDateString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

function sanitizeHtml(html) {
    const temp = document.createElement('div');
    temp.textContent = html;
    return temp.innerHTML;
}