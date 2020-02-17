export const onLoad = (): void => {
    addEventHandlers()
}

const addEventHandlers = (): void => {
    $('#filterButton').off('click.filterButton').on('click.filterButton', filterButtonClick)
    $('#clearButton').off('click.clearButton').on('click.clearButton', clearButtonClick)
    $(document).off('click.deleteTransaction', '.deleteTransaction').on('click.deleteTransaction', '.deleteTransaction', deleteTransactionClick)
}

const filterButtonClick = (): void => {
    $.blockUI()
}

const clearButtonClick = (ev: Event): void => {
    $(ev.target).closest('form').find(':input').val('')
}

const deleteTransactionClick = (ev: Event): void => {
    bootbox.confirm('Are you sure you want to delete this item?', (result: boolean) => {
        if (result) {
            $.blockUI()
            $(ev.target).closest('form').submit()
        }
    })
}
