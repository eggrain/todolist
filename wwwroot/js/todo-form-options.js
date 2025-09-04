(function () {
    if (window.__todoOptionsInitOnce) return;
    window.__todoOptionsInitOnce = true;

    let cachedOptions = null;
    let loadingPromise = null;

    async function getOptions() {
        if (cachedOptions) return cachedOptions;
        if (loadingPromise) return loadingPromise;

        loadingPromise = fetch("/Todos/Options", { credentials: "same-origin" })
            .then(r => { if (!r.ok) throw new Error("Failed to load options"); return r.json(); })
            .then(j => { cachedOptions = j; return cachedOptions; })
            .finally(() => { loadingPromise = null; });

        return loadingPromise;
    }

    async function populateSelect(sel, items, selectedId) {
        if (!sel) return;
        // If already populated (beyond the '— None —' option), skip
        if (sel.options && sel.options.length > 1) {
            sel.disabled = false;
            return;
        }
        items.forEach(x => {
            const opt = document.createElement("option");
            opt.value = x.id;
            opt.textContent = x.name;
            sel.appendChild(opt);
        });
        sel.disabled = false;

        if (selectedId) {
            const wanted = String(selectedId).toLowerCase();
            for (const o of sel.options) {
                if (String(o.value).toLowerCase() === wanted) {
                    o.selected = true;
                    break;
                }
            }
        }
    }

    async function initTodoForm(container) {
        if (!container || container.dataset.todoFormInitialized === "1") return;
        container.dataset.todoFormInitialized = "1";

        const projectSel =
            container.querySelector('select[name="Todo.ProjectId"]') ||
            container.querySelector("#projectSelect");

        const goalSel =
            container.querySelector('select[name="Todo.GoalId"]') ||
            container.querySelector("#goalSelect");

        const selectedProject = projectSel?.dataset.selectedProjectId;
        const selectedGoal = goalSel?.dataset.selectedGoalId;

        const load = async () => {
            try {
                const { projects, goals } = await getOptions();
                await populateSelect(projectSel, projects, selectedProject);
                await populateSelect(goalSel, goals, selectedGoal);
            } catch (e) {
                console.error(e);
            }
        };

        // 🔑 Disabled selects won't fire focus/click. For full-page forms, load now:
        const isVisible = !!(container.offsetParent || container.getClientRects().length);
        if (isVisible) { load(); }

        // Lazy-load on first interaction (works if you later enable them before interaction)
        projectSel?.addEventListener("focus", load, { once: true });
        projectSel?.addEventListener("click", load, { once: true });
        goalSel?.addEventListener("focus", load, { once: true });
        goalSel?.addEventListener("click", load, { once: true });

        // If inside a Bootstrap modal, load when it opens
        document.addEventListener("shown.bs.modal", (ev) => {
            if (ev.target && ev.target.contains(container)) load();
        });
    }

    // Auto-init any already-rendered forms (full-page New/Edit)
    document.addEventListener("DOMContentLoaded", () => {
        document.querySelectorAll('[data-todo-form]').forEach(initTodoForm);
    });

    // Expose for late-mounted partials
    window.initTodoForm = initTodoForm;
})();
