export const onLoad = (): void => {
    addEventHandlers()
}

const addEventHandlers = (): void => {
    $(document).off('click.deleteTransaction', '.deleteTransaction').on('click.deleteTransaction', '.deleteTransaction', deleteTransactionClick)
}

const deleteTransactionClick = (ev: Event): void => {
    bootbox.confirm('Are you sure you want to delete this item?', (result: boolean) => {
        if (result) {
            $.blockUI()
            $(ev.target).closest('form').submit()
        }
    })
}
