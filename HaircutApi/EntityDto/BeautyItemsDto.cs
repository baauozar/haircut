﻿namespace HaircutApi.EntityDto
{
    public class BeautyItemsDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DisplayOrder { get; set; }
    }
}