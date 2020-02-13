export const onLoad = (): void => {
    addEventHandlers()
}

const addEventHandlers = (): void => {
    $(document).off('click.deleteTransaction', '.deleteTransaction').on('click.deleteTransaction', '.deleteTransaction', deleteTransactionClick)
}

const deleteTransactionClick = (): void => {
    console.log('deleteTransactionClick')
}
